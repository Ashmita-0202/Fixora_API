using Fix.Authentication.Application.ServiceInterface;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Commands.MarkAllNotificationsAsRead
{
    public class MarkAllNotificationsAsReadCommandHandler : IRequestHandler<MarkAllNotificationsAsReadCommand, Result<string>>
    {
        private readonly INotificationServices _notificationServices;

        public MarkAllNotificationsAsReadCommandHandler(INotificationServices notificationServices)
        {
            _notificationServices = notificationServices;
        }

        public async Task<Result<string>> Handle(MarkAllNotificationsAsReadCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _notificationServices.MarkAllAsRead(request.UserId);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while marking all notifications as read", ex);
            }
        }
    }
}
