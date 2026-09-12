using Fix.Authentication.Application.Authentication.Commands.AddUser;
using Fix.Authentication.Application.Authentication.Commands.Login;
using Fix.Authentication.Application.Authentication.Commands.ResetPassword;
using Fix.Authentication.Application.Authentication.Common;
using Fix.Shared.Domain.Abstractions;

namespace Fix.Authentication.Application.ServiceInterface
{
    public interface IUserServices
    {
        Task<Result<AddUserCommandResponse>> Add(AddUserCommand request);

        Task<Result<LoginCommandResponse>> Login(LoginCommand request);

        Task GenerateResetToken(string email);

        Task<Result<ResetPasswordCommandResponse>> ResetPassword(string email, string token, string newPassword);

        Task<Result<MyProfileResponse>> GetProfile(string userId);

        Task<Result<MyProfileResponse>> UpdateProfile(string userId, string firstName, string lastName, string phone, string address);

        Task<Result<MyProfileResponse>> ChangePassword(string userId, string currentPassword, string newPassword);
    }
}
