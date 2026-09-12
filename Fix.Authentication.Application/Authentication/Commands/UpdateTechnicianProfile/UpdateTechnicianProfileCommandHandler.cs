using Fix.Authentication.Application.Authentication.Common;
using Fix.Authentication.Application.ServiceInterface;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Commands.UpdateTechnicianProfile
{
    public class UpdateTechnicianProfileCommandHandler : IRequestHandler<UpdateTechnicianProfileCommand, Result<TechnicianResponse>>
    {
        private readonly ITechnicianServices _technicianServices;

        public UpdateTechnicianProfileCommandHandler(ITechnicianServices technicianServices)
        {
            _technicianServices = technicianServices;
        }

        public async Task<Result<TechnicianResponse>> Handle(UpdateTechnicianProfileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _technicianServices.UpdateProfile(request.UserId, request.Specialty, request.City, request.ExperienceYears, request.Certification);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating your technician profile", ex);
            }
        }
    }
}
