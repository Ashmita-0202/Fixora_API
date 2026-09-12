using Fix.Authentication.Application.Authentication.Common;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Commands.ChangePassword
{
    public record ChangePasswordCommand(string UserId, string CurrentPassword, string NewPassword) : IRequest<Result<MyProfileResponse>>;
}
