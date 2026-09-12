using Fix.Authentication.Application.Authentication.Common;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Commands.ToggleTechnicianActiveStatus
{
    public record ToggleTechnicianActiveStatusCommand(string ProfileId) : IRequest<Result<TechnicianResponse>>;
}
