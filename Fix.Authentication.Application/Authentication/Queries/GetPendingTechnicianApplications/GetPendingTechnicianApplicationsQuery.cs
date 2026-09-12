using Fix.Authentication.Application.Authentication.Common;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Queries.GetPendingTechnicianApplications
{
    public record GetPendingTechnicianApplicationsQuery() : IRequest<Result<List<TechnicianResponse>>>;
}
