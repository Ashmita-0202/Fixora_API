using Fix.Authentication.Application.Authentication.Common;
using Fix.Shared.Domain.Abstractions;

namespace Fix.Authentication.Application.ServiceInterface
{
    public interface IAdminServices
    {
        Task<Result<DashboardStatsResponse>> GetDashboardStats();

        Task<Result<List<CustomerSummaryResponse>>> GetAllCustomers();
    }
}
