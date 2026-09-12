namespace Fix.Authentication.Application.Authentication.Commands.ChangePassword
{
    public record ChangePasswordRequest(string CurrentPassword, string NewPassword);
}
