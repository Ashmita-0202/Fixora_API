using Fix.Authentication.Application.Authentication.Common;
using Fix.Authentication.Application.ServiceInterface;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Commands.UpdateProfile
{
    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, Result<MyProfileResponse>>
    {
        private readonly IUserServices _userServices;

        public UpdateProfileCommandHandler(IUserServices userServices)
        {
            _userServices = userServices;
        }

        public async Task<Result<MyProfileResponse>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _userServices.UpdateProfile(request.UserId, request.FirstName, request.LastName, request.Phone, request.Address);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating profile", ex);
            }
        }
    }
}
