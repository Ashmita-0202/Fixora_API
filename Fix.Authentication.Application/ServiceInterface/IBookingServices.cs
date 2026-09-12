using Fix.Authentication.Application.Authentication.Common;
using Fix.Shared.Domain.Abstractions;

namespace Fix.Authentication.Application.ServiceInterface
{
    public interface IBookingServices
    {
        Task<Result<BookingResponse>> Create(string customerId, string serviceCategoryId, DateTime scheduledDate, string scheduledTime, string address, string? notes);

        Task<Result<List<BookingResponse>>> GetMyBookings(string customerId, string? status);

        Task<Result<List<BookingResponse>>> GetJobRequests();

        Task<Result<List<BookingResponse>>> GetMyJobs(string technicianId, string? status);

        Task<Result<List<BookingResponse>>> GetAll(string? status);

        Task<Result<BookingResponse>> GetById(string id, string requesterId, string requesterRole);

        Task<Result<BookingResponse>> AcceptJob(string bookingId, string technicianId);

        Task<Result<BookingResponse>> UpdateJobStatus(string bookingId, string technicianId, string newStatus);

        Task<Result<BookingResponse>> Cancel(string bookingId, string customerId);
    }
}
