using Fix.Authentication.Application.Authentication.Common;
using Fix.Authentication.Application.ServiceInterface;
using Fix.Shared.Domain.Abstractions;
using Fix.Shared.Domain.IRepository;
using Fix.Shared.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fix.Authentication.Infrastructure.ServiceImpl
{
    public class AdminServices : IAdminServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<DashboardStatsResponse>> GetDashboardStats()
        {
            try
            {
                var usersQuery = await _unitOfWork.BaseRepository<User>().GetAllAsync();
                var totalCustomers = await usersQuery.CountAsync(u => u.UserRole == UserRole.Customer);

                var techProfilesQuery = await _unitOfWork.BaseRepository<TechnicianProfile>().GetAllAsync();
                var totalTechnicians = await techProfilesQuery.CountAsync(p => p.ApprovalStatus == TechnicianApprovalStatus.Approved);
                var pendingApplications = await techProfilesQuery.CountAsync(p => p.ApprovalStatus == TechnicianApprovalStatus.Pending);

                var bookingsQuery = await _unitOfWork.BaseRepository<Booking>().GetAllAsync();
                var bookings = await bookingsQuery.AsNoTracking().ToListAsync();

                var pending = bookings.Count(b => b.Status == BookingStatus.Pending);
                var scheduled = bookings.Count(b => b.Status == BookingStatus.Scheduled);
                var inProgress = bookings.Count(b => b.Status == BookingStatus.InProgress);
                var completed = bookings.Count(b => b.Status == BookingStatus.Completed);
                var cancelled = bookings.Count(b => b.Status == BookingStatus.Cancelled);
                var totalRevenue = bookings.Where(b => b.Status == BookingStatus.Completed).Sum(b => b.Amount);

                var completionEligible = bookings.Count - cancelled;
                var completionRate = completionEligible > 0
                    ? Math.Round((double)completed / completionEligible * 100, 1)
                    : 0;

                var response = new DashboardStatsResponse
                {
                    TotalCustomers = totalCustomers,
                    TotalTechnicians = totalTechnicians,
                    PendingTechnicianApplications = pendingApplications,
                    TotalBookings = bookings.Count,
                    PendingBookings = pending,
                    ScheduledBookings = scheduled,
                    InProgressBookings = inProgress,
                    CompletedBookings = completed,
                    CancelledBookings = cancelled,
                    TotalRevenue = totalRevenue,
                    CompletionRatePercent = completionRate
                };

                return Result<DashboardStatsResponse>.Success(response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching dashboard stats", ex);
            }
        }

        public async Task<Result<List<CustomerSummaryResponse>>> GetAllCustomers()
        {
            try
            {
                var usersQuery = await _unitOfWork.BaseRepository<User>().GetAllAsync();
                var customers = await usersQuery
                    .Where(u => u.UserRole == UserRole.Customer)
                    .AsNoTracking()
                    .ToListAsync();

                if (customers.Count == 0)
                {
                    return Result<List<CustomerSummaryResponse>>.Success(new List<CustomerSummaryResponse>());
                }

                var customerIds = customers.Select(c => c.Id.ToString()).ToList();
                var bookingCounts = (await _unitOfWork.BaseRepository<Booking>()
                        .GetConditionalAsync(b => customerIds.Contains(b.CustomerId)))
                    .GroupBy(b => b.CustomerId)
                    .ToDictionary(g => g.Key, g => g.Count());

                var response = customers.Select(c => new CustomerSummaryResponse
                {
                    Id = c.Id.ToString(),
                    Name = $"{c.FirstName} {c.LastName}".Trim(),
                    Email = c.Email,
                    Phone = c.Phone,
                    Address = c.Address,
                    TotalBookings = bookingCounts.TryGetValue(c.Id.ToString(), out var count) ? count : 0,
                    JoinedAt = c.CreatedAt
                }).ToList();

                return Result<List<CustomerSummaryResponse>>.Success(response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching customers", ex);
            }
        }
    }
}
