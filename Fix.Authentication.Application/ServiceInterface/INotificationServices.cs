using Fix.Authentication.Application.Authentication.Common;
using Fix.Shared.Domain.Abstractions;

namespace Fix.Authentication.Application.ServiceInterface
{
    public interface INotificationServices
    {
        // Called internally by other services (BookingServices, TechnicianServices, etc.)
        // at key lifecycle events - not exposed directly as an API endpoint.
        Task Create(string userId, string type, string title, string body);

        Task<Result<List<NotificationResponse>>> GetMine(string userId, bool? unreadOnly);

        Task<Result<NotificationResponse>> MarkAsRead(string notificationId, string userId);

        Task<Result<string>> MarkAllAsRead(string userId);
    }
}
