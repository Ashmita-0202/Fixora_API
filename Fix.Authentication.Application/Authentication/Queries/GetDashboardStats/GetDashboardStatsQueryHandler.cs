using Fix.Authentication.Application.Authentication.Common;
using Fix.Authentication.Application.ServiceInterface;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Queries.GetDashboardStats
{
    public class GetDashboardStatsQueryHandler : IRequestHandler<GetDashboardStatsQuery, Result<DashboardStatsResponse>>
    {
        private readonly IAdminServices _adminServices;

        public GetDashboardStatsQueryHandler(IAdminServices adminServices)
        {
            _adminServices = adminServices;
        }

        public async Task<Result<DashboardStatsResponse>> Handle(GetDashboardStatsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _adminServices.GetDashboardStats();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching dashboard stats", ex);
            }
        }
    }
}
