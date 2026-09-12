using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Commands.MarkAllNotificationsAsRead
{
    public record MarkAllNotificationsAsReadCommand(string UserId) : IRequest<Result<string>>;
}
