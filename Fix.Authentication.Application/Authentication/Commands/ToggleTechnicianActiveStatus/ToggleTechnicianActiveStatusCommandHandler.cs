using Fix.Authentication.Application.Authentication.Common;
using Fix.Authentication.Application.ServiceInterface;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Commands.ToggleTechnicianActiveStatus
{
    public class ToggleTechnicianActiveStatusCommandHandler : IRequestHandler<ToggleTechnicianActiveStatusCommand, Result<TechnicianResponse>>
    {
        private readonly ITechnicianServices _technicianServices;

        public ToggleTechnicianActiveStatusCommandHandler(ITechnicianServices technicianServices)
        {
            _technicianServices = technicianServices;
        }

        public async Task<Result<TechnicianResponse>> Handle(ToggleTechnicianActiveStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _technicianServices.ToggleActiveStatus(request.ProfileId);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating technician status", ex);
            }
        }
    }
}
