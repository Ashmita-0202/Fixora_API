using Fix.Authentication.Application.Authentication.Common;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Commands.ApproveTechnician
{
    public record ApproveTechnicianCommand(string ProfileId) : IRequest<Result<TechnicianResponse>>;
}
