using Fix.Authentication.Application.Authentication.Common;
using Fix.Authentication.Application.ServiceInterface;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Commands.RejectTechnician
{
    public class RejectTechnicianCommandHandler : IRequestHandler<RejectTechnicianCommand, Result<TechnicianResponse>>
    {
        private readonly ITechnicianServices _technicianServices;

        public RejectTechnicianCommandHandler(ITechnicianServices technicianServices)
        {
            _technicianServices = technicianServices;
        }

        public async Task<Result<TechnicianResponse>> Handle(RejectTechnicianCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _technicianServices.Reject(request.ProfileId, request.Reason);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while rejecting the technician", ex);
            }
        }
    }
}
