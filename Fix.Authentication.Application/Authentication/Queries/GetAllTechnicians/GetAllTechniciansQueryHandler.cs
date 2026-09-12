using Fix.Authentication.Application.Authentication.Common;
using Fix.Authentication.Application.ServiceInterface;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Queries.GetAllTechnicians
{
    public class GetAllTechniciansQueryHandler : IRequestHandler<GetAllTechniciansQuery, Result<List<TechnicianResponse>>>
    {
        private readonly ITechnicianServices _technicianServices;

        public GetAllTechniciansQueryHandler(ITechnicianServices technicianServices)
        {
            _technicianServices = technicianServices;
        }

        public async Task<Result<List<TechnicianResponse>>> Handle(GetAllTechniciansQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _technicianServices.GetAll(request.ActiveOnly);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching technicians", ex);
            }
        }
    }
}
