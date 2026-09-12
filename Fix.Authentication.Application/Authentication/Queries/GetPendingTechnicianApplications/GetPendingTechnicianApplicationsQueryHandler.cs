using Fix.Authentication.Application.Authentication.Common;
using Fix.Authentication.Application.ServiceInterface;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Queries.GetPendingTechnicianApplications
{
    public class GetPendingTechnicianApplicationsQueryHandler : IRequestHandler<GetPendingTechnicianApplicationsQuery, Result<List<TechnicianResponse>>>
    {
        private readonly ITechnicianServices _technicianServices;

        public GetPendingTechnicianApplicationsQueryHandler(ITechnicianServices technicianServices)
        {
            _technicianServices = technicianServices;
        }

        public async Task<Result<List<TechnicianResponse>>> Handle(GetPendingTechnicianApplicationsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _technicianServices.GetPendingApplications();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching pending technician applications", ex);
            }
        }
    }
}
