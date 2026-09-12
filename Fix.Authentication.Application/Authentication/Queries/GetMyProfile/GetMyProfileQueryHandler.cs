using Fix.Authentication.Application.Authentication.Common;
using Fix.Authentication.Application.ServiceInterface;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Queries.GetMyProfile
{
    public class GetMyProfileQueryHandler : IRequestHandler<GetMyProfileQuery, Result<MyProfileResponse>>
    {
        private readonly IUserServices _userServices;

        public GetMyProfileQueryHandler(IUserServices userServices)
        {
            _userServices = userServices;
        }

        public async Task<Result<MyProfileResponse>> Handle(GetMyProfileQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _userServices.GetProfile(request.UserId);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching profile", ex);
            }
        }
    }
}
