using Fix.Authentication.Application.Authentication.Common;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Queries.GetMyProfile
{
    public record GetMyProfileQuery(string UserId) : IRequest<Result<MyProfileResponse>>;
}
