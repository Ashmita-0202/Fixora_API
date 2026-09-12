using Fix.Authentication.Application.Authentication.Common;
using Fix.Authentication.Application.ServiceInterface;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result<MyProfileResponse>>
    {
        private readonly IUserServices _userServices;

        public ChangePasswordCommandHandler(IUserServices userServices)
        {
            _userServices = userServices;
        }

        public async Task<Result<MyProfileResponse>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _userServices.ChangePassword(request.UserId, request.CurrentPassword, request.NewPassword);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while changing password", ex);
            }
        }
    }
}
