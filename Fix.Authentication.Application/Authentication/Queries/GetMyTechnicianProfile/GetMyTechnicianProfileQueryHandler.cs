using Fix.Authentication.Application.Authentication.Common;
using Fix.Authentication.Application.ServiceInterface;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Queries.GetMyTechnicianProfile
{
    public class GetMyTechnicianProfileQueryHandler : IRequestHandler<GetMyTechnicianProfileQuery, Result<TechnicianResponse>>
    {
        private readonly ITechnicianServices _technicianServices;

        public GetMyTechnicianProfileQueryHandler(ITechnicianServices technicianServices)
        {
            _technicianServices = technicianServices;
        }

        public async Task<Result<TechnicianResponse>> Handle(GetMyTechnicianProfileQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _technicianServices.GetByUserId(request.UserId);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching your technician profile", ex);
            }
        }
    }
}
