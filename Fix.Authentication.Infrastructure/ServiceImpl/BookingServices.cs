using System.Transactions;
using Fix.Authentication.Application.Authentication.Common;
using Fix.Authentication.Application.ServiceInterface;
using Fix.Shared.Domain.Abstractions;
using Fix.Shared.Domain.IRepository;
using Fix.Shared.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fix.Authentication.Infrastructure.ServiceImpl
{
    public class BookingServices : IBookingServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationServices _notificationServices;

        public BookingServices(IUnitOfWork unitOfWork, INotificationServices notificationServices)
        {
            _unitOfWork = unitOfWork;
            _notificationServices = notificationServices;
        }

        public async Task<Result<BookingResponse>> Create(string customerId, string serviceCategoryId, DateTime scheduledDate, string scheduledTime, string address, string? notes)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var service = await _unitOfWork.BaseRepository<ServiceCategory>()
                        .GetSingleAsync(s => s.Id == serviceCategoryId);

                    if (service is null || !service.IsActive)
                    {
                        return Result<BookingResponse>.Failure("NotFound: Selected service is not available.");
                    }

                    if (scheduledDate.Date < DateTime.UtcNow.Date)
                    {
                        return Result<BookingResponse>.Failure("Scheduled date cannot be in the past.");
                    }

                    var booking = new Booking(
                        Guid.NewGuid().ToString(),
                        customerId,
                        serviceCategoryId,
                        scheduledDate,
                        scheduledTime,
                        address,
                        service.BaseServiceCharge,
                        notes
                    );

                    await _unitOfWork.BaseRepository<Booking>().AddAsync(booking);
                    await _unitOfWork.SaveChangesAsync();
                    scope.Complete();

                    var response = await ToResponse(booking);
                    return Result<BookingResponse>.Success(response);
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw new Exception("An error occurred while creating the booking", ex);
                }
            }
        }

        public async Task<Result<List<BookingResponse>>> GetMyBookings(string customerId, string? status)
        {
            try
            {
                var query = await _unitOfWork.BaseRepository<Booking>().GetAllAsync();
                query = query.Where(b => b.CustomerId == customerId);

                if (!string.IsNullOrEmpty(status) && Enum.TryParse<BookingStatus>(status, true, out var parsedStatus))
                {
                    query = query.Where(b => b.Status == parsedStatus);
                }

                var bookings = await query.AsNoTracking().OrderByDescending(b => b.CreatedAt).ToListAsync();
                var response = await ToResponseList(bookings);
                return Result<List<BookingResponse>>.Success(response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching your bookings", ex);
            }
        }

        public async Task<Result<List<BookingResponse>>> GetJobRequests()
        {
            try
            {
                // Unassigned, still-pending bookings any technician can pick up.
                var query = await _unitOfWork.BaseRepository<Booking>().GetAllAsync();
                query = query.Where(b => b.Status == BookingStatus.Pending && b.TechnicianId == null);

                var bookings = await query.AsNoTracking().OrderBy(b => b.ScheduledDate).ToListAsync();
                var response = await ToResponseList(bookings);
                return Result<List<BookingResponse>>.Success(response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching job requests", ex);
            }
        }

        public async Task<Result<List<BookingResponse>>> GetMyJobs(string technicianId, string? status)
        {
            try
            {
                var query = await _unitOfWork.BaseRepository<Booking>().GetAllAsync();
                query = query.Where(b => b.TechnicianId == technicianId);

                if (!string.IsNullOrEmpty(status) && Enum.TryParse<BookingStatus>(status, true, out var parsedStatus))
                {
                    query = query.Where(b => b.Status == parsedStatus);
                }

                var bookings = await query.AsNoTracking().OrderBy(b => b.ScheduledDate).ToListAsync();
                var response = await ToResponseList(bookings);
                return Result<List<BookingResponse>>.Success(response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching your jobs", ex);
            }
        }

        public async Task<Result<List<BookingResponse>>> GetAll(string? status)
        {
            try
            {
                var query = await _unitOfWork.BaseRepository<Booking>().GetAllAsync();

                if (!string.IsNullOrEmpty(status) && Enum.TryParse<BookingStatus>(status, true, out var parsedStatus))
                {
                    query = query.Where(b => b.Status == parsedStatus);
                }

                var bookings = await query.AsNoTracking().OrderByDescending(b => b.CreatedAt).ToListAsync();
                var response = await ToResponseList(bookings);
                return Result<List<BookingResponse>>.Success(response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching bookings", ex);
            }
        }

        public async Task<Result<BookingResponse>> GetById(string id, string requesterId, string requesterRole)
        {
            try
            {
                var booking = await _unitOfWork.BaseRepository<Booking>().GetSingleAsync(b => b.Id == id);

                if (booking is null)
                {
                    return Result<BookingResponse>.Failure("NotFound: Booking not found.");
                }

                var isOwner = booking.CustomerId == requesterId || booking.TechnicianId == requesterId;
                var isAdmin = string.Equals(requesterRole, "Admin", StringComparison.OrdinalIgnoreCase);

                if (!isOwner && !isAdmin)
                {
                    return Result<BookingResponse>.Failure("Unauthorized: You do not have access to this booking.");
                }

                var response = await ToResponse(booking);
                return Result<BookingResponse>.Success(response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching the booking", ex);
            }
        }

        public async Task<Result<BookingResponse>> AcceptJob(string bookingId, string technicianId)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var booking = await _unitOfWork.BaseRepository<Booking>().GetSingleAsync(b => b.Id == bookingId);

                    if (booking is null)
                    {
                        return Result<BookingResponse>.Failure("NotFound: Booking not found.");
                    }

                    if (booking.Status != BookingStatus.Pending || booking.TechnicianId != null)
                    {
                        return Result<BookingResponse>.Failure("Conflict: This job has already been accepted by another technician.");
                    }

                    var techProfile = await _unitOfWork.BaseRepository<TechnicianProfile>()
                        .GetSingleAsync(p => p.UserId == technicianId);

                    if (techProfile is null || techProfile.ApprovalStatus != TechnicianApprovalStatus.Approved || !techProfile.IsActive)
                    {
                        return Result<BookingResponse>.Failure("Unauthorized: Your technician account is not approved/active yet.");
                    }

                    booking.TechnicianId = technicianId;
                    booking.Status = BookingStatus.Scheduled;
                    booking.UpdatedAt = DateTime.UtcNow;

                    _unitOfWork.BaseRepository<Booking>().Update(booking);
                    await _unitOfWork.SaveChangesAsync();
                    scope.Complete();

                    await _notificationServices.Create(
                        booking.CustomerId, "info", "Technician Assigned",
                        "A technician has accepted your booking and it's now scheduled.");

                    var response = await ToResponse(booking);
                    return Result<BookingResponse>.Success(response);
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw new Exception("An error occurred while accepting the job", ex);
                }
            }
        }

        public async Task<Result<BookingResponse>> UpdateJobStatus(string bookingId, string technicianId, string newStatus)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (!Enum.TryParse<BookingStatus>(newStatus, true, out var targetStatus))
                    {
                        return Result<BookingResponse>.Failure("Invalid status value.");
                    }

                    var booking = await _unitOfWork.BaseRepository<Booking>().GetSingleAsync(b => b.Id == bookingId);

                    if (booking is null)
                    {
                        return Result<BookingResponse>.Failure("NotFound: Booking not found.");
                    }

                    if (booking.TechnicianId != technicianId)
                    {
                        return Result<BookingResponse>.Failure("Unauthorized: This job is not assigned to you.");
                    }

                    // Only forward transitions are allowed, in this order.
                    var validNextSteps = new Dictionary<BookingStatus, BookingStatus[]>
                    {
                        [BookingStatus.Scheduled] = new[] { BookingStatus.InProgress },
                        [BookingStatus.InProgress] = new[] { BookingStatus.Completed }
                    };

                    if (!validNextSteps.TryGetValue(booking.Status, out var allowedNext) || !allowedNext.Contains(targetStatus))
                    {
                        return Result<BookingResponse>.Failure($"Cannot move a job from {booking.Status} to {targetStatus}.");
                    }

                    booking.Status = targetStatus;
                    booking.UpdatedAt = DateTime.UtcNow;
                    if (targetStatus == BookingStatus.Completed)
                    {
                        booking.CompletedAt = DateTime.UtcNow;
                    }

                    _unitOfWork.BaseRepository<Booking>().Update(booking);
                    await _unitOfWork.SaveChangesAsync();
                    scope.Complete();

                    if (targetStatus == BookingStatus.Completed)
                    {
                        await _notificationServices.Create(
                            booking.CustomerId, "success", "Repair Completed",
                            "Your repair has been marked as completed. Rate your experience!");
                    }

                    var response = await ToResponse(booking);
                    return Result<BookingResponse>.Success(response);
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw new Exception("An error occurred while updating job status", ex);
                }
            }
        }

        public async Task<Result<BookingResponse>> Cancel(string bookingId, string customerId)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var booking = await _unitOfWork.BaseRepository<Booking>().GetSingleAsync(b => b.Id == bookingId);

                    if (booking is null)
                    {
                        return Result<BookingResponse>.Failure("NotFound: Booking not found.");
                    }

                    if (booking.CustomerId != customerId)
                    {
                        return Result<BookingResponse>.Failure("Unauthorized: This is not your booking.");
                    }

                    if (booking.Status is BookingStatus.InProgress or BookingStatus.Completed or BookingStatus.Cancelled)
                    {
                        return Result<BookingResponse>.Failure($"Cannot cancel a booking that is already {booking.Status}.");
                    }

                    booking.Status = BookingStatus.Cancelled;
                    booking.UpdatedAt = DateTime.UtcNow;

                    _unitOfWork.BaseRepository<Booking>().Update(booking);
                    await _unitOfWork.SaveChangesAsync();
                    scope.Complete();

                    if (booking.TechnicianId is not null)
                    {
                        await _notificationServices.Create(
                            booking.TechnicianId, "warning", "Booking Cancelled",
                            "A booking assigned to you has been cancelled by the customer.");
                    }

                    var response = await ToResponse(booking);
                    return Result<BookingResponse>.Success(response);
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw new Exception("An error occurred while cancelling the booking", ex);
                }
            }
        }

        // ---- Helpers: joins Booking with ServiceCategory/User names in memory,
        // since the generic repository doesn't expose cross-entity SQL joins. ----

        private async Task<BookingResponse> ToResponse(Booking booking)
        {
            var list = await ToResponseList(new List<Booking> { booking });
            return list.Single();
        }

        private async Task<List<BookingResponse>> ToResponseList(List<Booking> bookings)
        {
            if (bookings.Count == 0)
            {
                return new List<BookingResponse>();
            }

            var serviceIds = bookings.Select(b => b.ServiceCategoryId).Distinct().ToList();
            var userIds = bookings.Select(b => b.CustomerId)
                .Concat(bookings.Where(b => b.TechnicianId != null).Select(b => b.TechnicianId!))
                .Distinct()
                .ToList();
            var userGuids = userIds.Select(Guid.Parse).ToList();

            var services = (await _unitOfWork.BaseRepository<ServiceCategory>()
                    .GetConditionalAsync(s => serviceIds.Contains(s.Id)))
                .ToDictionary(s => s.Id, s => s.CategoryName);

            var users = (await _unitOfWork.BaseRepository<User>()
                    .GetConditionalAsync(u => userGuids.Contains(u.Id)))
                .ToDictionary(u => u.Id.ToString(), u => $"{u.FirstName} {u.LastName}".Trim());

            return bookings.Select(b => new BookingResponse
            {
                Id = b.Id,
                ServiceCategoryId = b.ServiceCategoryId,
                ServiceName = services.TryGetValue(b.ServiceCategoryId, out var sName) ? sName : "Unknown Service",
                CustomerId = b.CustomerId,
                CustomerName = users.TryGetValue(b.CustomerId, out var cName) ? cName : "Unknown Customer",
                TechnicianId = b.TechnicianId,
                TechnicianName = b.TechnicianId != null && users.TryGetValue(b.TechnicianId, out var tName) ? tName : null,
                ScheduledDate = b.ScheduledDate,
                ScheduledTime = b.ScheduledTime,
                Address = b.Address,
                Amount = b.Amount,
                Notes = b.Notes,
                Status = b.Status.ToString(),
                CreatedAt = b.CreatedAt,
                CompletedAt = b.CompletedAt
            }).ToList();
        }
    }
}
