using System.Transactions;
using Fix.Authentication.Application.Authentication.Common;
using Fix.Authentication.Application.ServiceInterface;
using Fix.Shared.Domain.Abstractions;
using Fix.Shared.Domain.IRepository;
using Fix.Shared.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fix.Authentication.Infrastructure.ServiceImpl
{
    public class ReviewServices : IReviewServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReviewServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<ReviewResponse>> Create(string customerId, string bookingId, int rating, string? comment)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var booking = await _unitOfWork.BaseRepository<Booking>().GetSingleAsync(b => b.Id == bookingId);

                    if (booking is null)
                    {
                        return Result<ReviewResponse>.Failure("NotFound: Booking not found.");
                    }

                    if (booking.CustomerId != customerId)
                    {
                        return Result<ReviewResponse>.Failure("Unauthorized: This is not your booking.");
                    }

                    if (booking.Status != BookingStatus.Completed)
                    {
                        return Result<ReviewResponse>.Failure("You can only review a completed job.");
                    }

                    if (booking.TechnicianId is null)
                    {
                        return Result<ReviewResponse>.Failure("This booking has no assigned technician to review.");
                    }

                    var alreadyReviewed = await _unitOfWork.BaseRepository<Review>()
                        .AnyAsync(r => r.BookingId == bookingId);

                    if (alreadyReviewed)
                    {
                        return Result<ReviewResponse>.Failure("Conflict: You've already reviewed this booking.");
                    }

                    var review = new Review(
                        Guid.NewGuid().ToString(),
                        bookingId,
                        customerId,
                        booking.TechnicianId,
                        rating,
                        comment
                    );

                    await _unitOfWork.BaseRepository<Review>().AddAsync(review);
                    await _unitOfWork.SaveChangesAsync();
                    scope.Complete();

                    var response = (await ToResponseList(new List<Review> { review })).Single();
                    return Result<ReviewResponse>.Success(response);
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw new Exception("An error occurred while submitting the review", ex);
                }
            }
        }

        public async Task<Result<List<ReviewResponse>>> GetForTechnician(string technicianId)
        {
            try
            {
                var query = await _unitOfWork.BaseRepository<Review>().GetAllAsync();
                var reviews = await query
                    .Where(r => r.TechnicianId == technicianId)
                    .AsNoTracking()
                    .OrderByDescending(r => r.CreatedAt)
                    .ToListAsync();

                var response = await ToResponseList(reviews);
                return Result<List<ReviewResponse>>.Success(response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching reviews", ex);
            }
        }

        public async Task<(double AverageRating, int ReviewCount)> GetRatingSummary(string technicianId)
        {
            var summaries = await GetRatingSummaries(new List<string> { technicianId });
            return summaries.TryGetValue(technicianId, out var summary) ? summary : (0, 0);
        }

        public async Task<Dictionary<string, (double AverageRating, int ReviewCount)>> GetRatingSummaries(List<string> technicianIds)
        {
            if (technicianIds.Count == 0)
            {
                return new Dictionary<string, (double, int)>();
            }

            var reviews = await _unitOfWork.BaseRepository<Review>()
                .GetConditionalAsync(r => technicianIds.Contains(r.TechnicianId));

            return reviews
                .GroupBy(r => r.TechnicianId)
                .ToDictionary(
                    g => g.Key,
                    g => (Math.Round(g.Average(r => r.Rating), 1), g.Count())
                );
        }

        private async Task<List<ReviewResponse>> ToResponseList(List<Review> reviews)
        {
            if (reviews.Count == 0)
            {
                return new List<ReviewResponse>();
            }

            var userGuids = reviews.Select(r => Guid.Parse(r.CustomerId)).Distinct().ToList();
            var users = (await _unitOfWork.BaseRepository<User>()
                    .GetConditionalAsync(u => userGuids.Contains(u.Id)))
                .ToDictionary(u => u.Id.ToString(), u => $"{u.FirstName} {u.LastName}".Trim());

            return reviews.Select(r => new ReviewResponse
            {
                Id = r.Id,
                BookingId = r.BookingId,
                CustomerId = r.CustomerId,
                CustomerName = users.TryGetValue(r.CustomerId, out var name) ? name : "Unknown Customer",
                TechnicianId = r.TechnicianId,
                Rating = r.Rating,
                Comment = r.Comment,
                CreatedAt = r.CreatedAt
            }).ToList();
        }
    }
}
