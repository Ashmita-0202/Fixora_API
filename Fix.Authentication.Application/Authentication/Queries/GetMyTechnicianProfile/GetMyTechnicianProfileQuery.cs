using Fix.Authentication.Application.Authentication.Common;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Queries.GetMyTechnicianProfile
{
    public record GetMyTechnicianProfileQuery(string UserId) : IRequest<Result<TechnicianResponse>>;
}
