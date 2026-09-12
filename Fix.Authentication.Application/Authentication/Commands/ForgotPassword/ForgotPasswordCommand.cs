using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Commands.ForgotPassword
{
    public record ForgotPasswordCommand
    (
        string Email
    ) : IRequest<Result<ForgotPasswordCommandResponse>>;
}
