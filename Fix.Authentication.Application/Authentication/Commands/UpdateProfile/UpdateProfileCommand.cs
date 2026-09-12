using Fix.Authentication.Application.Authentication.Common;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Commands.UpdateProfile
{
    public record UpdateProfileCommand(string UserId, string FirstName, string LastName, string Phone, string Address) : IRequest<Result<MyProfileResponse>>;
}
