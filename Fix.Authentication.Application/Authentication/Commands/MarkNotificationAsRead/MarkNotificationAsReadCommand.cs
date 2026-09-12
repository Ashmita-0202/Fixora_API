using Fix.Authentication.Application.Authentication.Common;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Commands.MarkNotificationAsRead
{
    public record MarkNotificationAsReadCommand(string NotificationId, string UserId) : IRequest<Result<NotificationResponse>>;
}
