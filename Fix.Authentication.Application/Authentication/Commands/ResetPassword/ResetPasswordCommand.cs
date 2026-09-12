using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Commands.ResetPassword
{
    public record ResetPasswordCommand
    (
        string Email,
        string Token,
        string NewPassword
    ) : IRequest<Result<ResetPasswordCommandResponse>>;
}
