using System.Transactions;
using Fix.Authentication.Application.Authentication.Common;
using Fix.Authentication.Application.ServiceInterface;
using Fix.Shared.Domain.Abstractions;
using Fix.Shared.Domain.IRepository;
using Fix.Shared.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fix.Authentication.Infrastructure.ServiceImpl
{
    public class TechnicianServices : ITechnicianServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReviewServices _reviewServices;
        private readonly INotificationServices _notificationServices;

        public TechnicianServices(IUnitOfWork unitOfWork, IReviewServices reviewServices, INotificationServices notificationServices)
        {
            _unitOfWork = unitOfWork;
            _reviewServices = reviewServices;
            _notificationServices = notificationServices;
        }

        public async Task<Result<List<TechnicianResponse>>> GetPendingApplications()
        {
            try
            {
                var query = await _unitOfWork.BaseRepository<TechnicianProfile>().GetAllAsync();
                var pending = await query
                    .Where(p => p.ApprovalStatus == TechnicianApprovalStatus.Pending)
                    .AsNoTracking()
                    .OrderBy(p => p.AppliedAt)
                    .ToListAsync();

                var response = await ToResponseList(pending);
                return Result<List<TechnicianResponse>>.Success(response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching pending technician applications", ex);
            }
        }

        public async Task<Result<List<TechnicianResponse>>> GetAll(bool? activeOnly)
        {
            try
            {
                var query = await _unitOfWork.BaseRepository<TechnicianProfile>().GetAllAsync();
                query = query.Where(p => p.ApprovalStatus == TechnicianApprovalStatus.Approved);

                if (activeOnly == true)
                {
                    query = query.Where(p => p.IsActive);
                }

                var technicians = await query.AsNoTracking().ToListAsync();
                var response = await ToResponseList(technicians);
                return Result<List<TechnicianResponse>>.Success(response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching technicians", ex);
            }
        }

        public async Task<Result<TechnicianResponse>> GetByUserId(string userId)
        {
            try
            {
                var profile = await _unitOfWork.BaseRepository<TechnicianProfile>()
                    .GetSingleAsync(p => p.UserId == userId);

                if (profile is null)
                {
                    return Result<TechnicianResponse>.Failure("NotFound: Technician profile not found.");
                }

                var response = (await ToResponseList(new List<TechnicianProfile> { profile })).Single();
                return Result<TechnicianResponse>.Success(response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching the technician profile", ex);
            }
        }

        public async Task<Result<TechnicianResponse>> Approve(string profileId)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var profile = await _unitOfWork.BaseRepository<TechnicianProfile>()
                        .GetSingleAsync(p => p.Id == profileId);

                    if (profile is null)
                    {
                        return Result<TechnicianResponse>.Failure("NotFound: Technician application not found.");
                    }

                    profile.ApprovalStatus = TechnicianApprovalStatus.Approved;
                    profile.IsActive = true;
                    profile.RejectionReason = null;
                    profile.ReviewedAt = DateTime.UtcNow;

                    _unitOfWork.BaseRepository<TechnicianProfile>().Update(profile);
                    await _unitOfWork.SaveChangesAsync();
                    scope.Complete();

                    await _notificationServices.Create(
                        profile.UserId, "success", "Application Approved",
                        "Congratulations! Your technician application has been approved. You can now accept jobs.");

                    var response = (await ToResponseList(new List<TechnicianProfile> { profile })).Single();
                    return Result<TechnicianResponse>.Success(response);
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw new Exception("An error occurred while approving the technician", ex);
                }
            }
        }

        public async Task<Result<TechnicianResponse>> Reject(string profileId, string? reason)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var profile = await _unitOfWork.BaseRepository<TechnicianProfile>()
                        .GetSingleAsync(p => p.Id == profileId);

                    if (profile is null)
                    {
                        return Result<TechnicianResponse>.Failure("NotFound: Technician application not found.");
                    }

                    profile.ApprovalStatus = TechnicianApprovalStatus.Rejected;
                    profile.IsActive = false;
                    profile.RejectionReason = reason;
                    profile.ReviewedAt = DateTime.UtcNow;

                    _unitOfWork.BaseRepository<TechnicianProfile>().Update(profile);
                    await _unitOfWork.SaveChangesAsync();
                    scope.Complete();

                    await _notificationServices.Create(
                        profile.UserId, "warning", "Application Not Approved",
                        string.IsNullOrEmpty(reason)
                            ? "Your technician application was not approved."
                            : $"Your technician application was not approved. Reason: {reason}");

                    var response = (await ToResponseList(new List<TechnicianProfile> { profile })).Single();
                    return Result<TechnicianResponse>.Success(response);
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw new Exception("An error occurred while rejecting the technician", ex);
                }
            }
        }

        public async Task<Result<TechnicianResponse>> ToggleActiveStatus(string profileId)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var profile = await _unitOfWork.BaseRepository<TechnicianProfile>()
                        .GetSingleAsync(p => p.Id == profileId);

                    if (profile is null)
                    {
                        return Result<TechnicianResponse>.Failure("NotFound: Technician not found.");
                    }

                    if (profile.ApprovalStatus != TechnicianApprovalStatus.Approved)
                    {
                        return Result<TechnicianResponse>.Failure("Only approved technicians can be activated/deactivated.");
                    }

                    profile.IsActive = !profile.IsActive;

                    _unitOfWork.BaseRepository<TechnicianProfile>().Update(profile);
                    await _unitOfWork.SaveChangesAsync();
                    scope.Complete();

                    var response = (await ToResponseList(new List<TechnicianProfile> { profile })).Single();
                    return Result<TechnicianResponse>.Success(response);
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw new Exception("An error occurred while updating technician status", ex);
                }
            }
        }

        public async Task<Result<TechnicianResponse>> UpdateProfile(string userId, string specialty, string city, int experienceYears, string? certification)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var profile = await _unitOfWork.BaseRepository<TechnicianProfile>()
                        .GetSingleAsync(p => p.UserId == userId);

                    if (profile is null)
                    {
                        return Result<TechnicianResponse>.Failure("NotFound: Technician profile not found.");
                    }

                    profile.Specialty = specialty;
                    profile.City = city;
                    profile.ExperienceYears = experienceYears;
                    profile.Certification = certification;

                    _unitOfWork.BaseRepository<TechnicianProfile>().Update(profile);
                    await _unitOfWork.SaveChangesAsync();
                    scope.Complete();

                    var response = (await ToResponseList(new List<TechnicianProfile> { profile })).Single();
                    return Result<TechnicianResponse>.Success(response);
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw new Exception("An error occurred while updating your technician profile", ex);
                }
            }
        }

        public async Task<Result<EarningsResponse>> GetEarnings(string userId)
        {
            try
            {
                var query = await _unitOfWork.BaseRepository<Booking>().GetAllAsync();
                var completedJobs = await query
                    .Where(b => b.TechnicianId == userId && b.Status == BookingStatus.Completed)
                    .AsNoTracking()
                    .OrderByDescending(b => b.CompletedAt)
                    .ToListAsync();

                var now = DateTime.UtcNow;
                var thisMonthEarnings = completedJobs
                    .Where(b => b.CompletedAt.HasValue && b.CompletedAt.Value.Month == now.Month && b.CompletedAt.Value.Year == now.Year)
                    .Sum(b => b.Amount);

                var serviceIds = completedJobs.Select(b => b.ServiceCategoryId).Distinct().ToList();
                var services = (await _unitOfWork.BaseRepository<ServiceCategory>()
                        .GetConditionalAsync(s => serviceIds.Contains(s.Id)))
                    .ToDictionary(s => s.Id, s => s.CategoryName);

                var response = new EarningsResponse
                {
                    TotalEarnings = completedJobs.Sum(b => b.Amount),
                    ThisMonthEarnings = thisMonthEarnings,
                    JobsCompleted = completedJobs.Count,
                    RecentJobs = completedJobs.Take(20).Select(b => new EarningsJobItem
                    {
                        BookingId = b.Id,
                        ServiceName = services.TryGetValue(b.ServiceCategoryId, out var name) ? name : "Unknown Service",
                        Amount = b.Amount,
                        CompletedAt = b.CompletedAt ?? b.UpdatedAt ?? b.CreatedAt
                    }).ToList()
                };

                return Result<EarningsResponse>.Success(response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching earnings", ex);
            }
        }

        // ---- Helper: joins TechnicianProfile with User (name/email/phone) and
        // completed-booking count, in memory (see BookingServices for the same pattern). ----
        private async Task<List<TechnicianResponse>> ToResponseList(List<TechnicianProfile> profiles)
        {
            if (profiles.Count == 0)
            {
                return new List<TechnicianResponse>();
            }

            var userGuids = profiles.Select(p => Guid.Parse(p.UserId)).Distinct().ToList();
            var users = (await _unitOfWork.BaseRepository<User>()
                    .GetConditionalAsync(u => userGuids.Contains(u.Id)))
                .ToDictionary(u => u.Id.ToString(), u => u);

            var technicianIds = profiles.Select(p => p.UserId).ToList();
            var completedJobCounts = (await _unitOfWork.BaseRepository<Booking>()
                    .GetConditionalAsync(b => b.TechnicianId != null
                        && technicianIds.Contains(b.TechnicianId)
                        && b.Status == BookingStatus.Completed))
                .GroupBy(b => b.TechnicianId!)
                .ToDictionary(g => g.Key, g => g.Count());

            var ratingSummaries = await _reviewServices.GetRatingSummaries(technicianIds);

            return profiles.Select(p =>
            {
                users.TryGetValue(p.UserId, out var user);
                completedJobCounts.TryGetValue(p.UserId, out var jobsCompleted);
                ratingSummaries.TryGetValue(p.UserId, out var ratingSummary);

                return new TechnicianResponse
                {
                    Id = p.Id,
                    UserId = p.UserId,
                    Name = user is not null ? $"{user.FirstName} {user.LastName}".Trim() : "Unknown",
                    Email = user?.Email ?? string.Empty,
                    Phone = user?.Phone ?? string.Empty,
                    Specialty = p.Specialty,
                    City = p.City,
                    ExperienceYears = p.ExperienceYears,
                    Certification = p.Certification,
                    ApprovalStatus = p.ApprovalStatus.ToString(),
                    IsActive = p.IsActive,
                    RejectionReason = p.RejectionReason,
                    JobsCompleted = jobsCompleted,
                    AverageRating = ratingSummary.AverageRating,
                    ReviewCount = ratingSummary.ReviewCount,
                    AppliedAt = p.AppliedAt,
                    ReviewedAt = p.ReviewedAt
                };
            }).ToList();
        }
    }
}
