using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Commands.Login
{
    public record LoginCommand
    (
        string Email,
        string Password
    ) : IRequest<Result<LoginCommandResponse>>;
}
