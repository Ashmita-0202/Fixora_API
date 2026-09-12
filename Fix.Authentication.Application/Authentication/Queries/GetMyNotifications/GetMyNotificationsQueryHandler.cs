using Fix.Authentication.Application.Authentication.Common;
using Fix.Authentication.Application.ServiceInterface;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Queries.GetMyNotifications
{
    public class GetMyNotificationsQueryHandler : IRequestHandler<GetMyNotificationsQuery, Result<List<NotificationResponse>>>
    {
        private readonly INotificationServices _notificationServices;

        public GetMyNotificationsQueryHandler(INotificationServices notificationServices)
        {
            _notificationServices = notificationServices;
        }

        public async Task<Result<List<NotificationResponse>>> Handle(GetMyNotificationsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _notificationServices.GetMine(request.UserId, request.UnreadOnly);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching notifications", ex);
            }
        }
    }
}
