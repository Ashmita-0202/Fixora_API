using Fix.Authentication.Application.Authentication.Common;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Commands.UpdateTechnicianProfile
{
    public record UpdateTechnicianProfileCommand(string UserId, string Specialty, string City, int ExperienceYears, string? Certification) : IRequest<Result<TechnicianResponse>>;
}
