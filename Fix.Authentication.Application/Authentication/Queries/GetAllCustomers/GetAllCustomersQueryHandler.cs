using Fix.Authentication.Application.Authentication.Common;
using Fix.Authentication.Application.ServiceInterface;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Queries.GetAllCustomers
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, Result<List<CustomerSummaryResponse>>>
    {
        private readonly IAdminServices _adminServices;

        public GetAllCustomersQueryHandler(IAdminServices adminServices)
        {
            _adminServices = adminServices;
        }

        public async Task<Result<List<CustomerSummaryResponse>>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _adminServices.GetAllCustomers();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching customers", ex);
            }
        }
    }
}
