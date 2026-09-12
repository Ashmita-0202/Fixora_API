using Fix.Authentication.Application.Authentication.Common;
using Fix.Authentication.Application.ServiceInterface;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Commands.ApproveTechnician
{
    public class ApproveTechnicianCommandHandler : IRequestHandler<ApproveTechnicianCommand, Result<TechnicianResponse>>
    {
        private readonly ITechnicianServices _technicianServices;

        public ApproveTechnicianCommandHandler(ITechnicianServices technicianServices)
        {
            _technicianServices = technicianServices;
        }

        public async Task<Result<TechnicianResponse>> Handle(ApproveTechnicianCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _technicianServices.Approve(request.ProfileId);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while approving the technician", ex);
            }
        }
    }
}
