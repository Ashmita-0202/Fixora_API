using Fix.Authentication.Application.Authentication.Common;
using Fix.Authentication.Application.ServiceInterface;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Commands.MarkNotificationAsRead
{
    public class MarkNotificationAsReadCommandHandler : IRequestHandler<MarkNotificationAsReadCommand, Result<NotificationResponse>>
    {
        private readonly INotificationServices _notificationServices;

        public MarkNotificationAsReadCommandHandler(INotificationServices notificationServices)
        {
            _notificationServices = notificationServices;
        }

        public async Task<Result<NotificationResponse>> Handle(MarkNotificationAsReadCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _notificationServices.MarkAsRead(request.NotificationId, request.UserId);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while marking notification as read", ex);
            }
        }
    }
}
