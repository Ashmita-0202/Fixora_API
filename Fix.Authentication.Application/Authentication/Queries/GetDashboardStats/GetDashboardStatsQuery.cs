using Fix.Authentication.Application.Authentication.Common;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Queries.GetDashboardStats
{
    public record GetDashboardStatsQuery() : IRequest<Result<DashboardStatsResponse>>;
}
