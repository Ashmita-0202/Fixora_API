using Fix.Authentication.Application.Authentication.Common;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Commands.RejectTechnician
{
    public record RejectTechnicianCommand(string ProfileId, string? Reason) : IRequest<Result<TechnicianResponse>>;
}
