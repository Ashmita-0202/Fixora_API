using Fix.Authentication.Application.Authentication.Common;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Queries.GetMyNotifications
{
    public record GetMyNotificationsQuery(string UserId, bool? UnreadOnly) : IRequest<Result<List<NotificationResponse>>>;
}
