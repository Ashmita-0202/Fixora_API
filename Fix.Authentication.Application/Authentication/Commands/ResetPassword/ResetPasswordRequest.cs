namespace Fix.Authentication.Application.Authentication.Commands.ResetPassword
{
    public record ResetPasswordRequest
    (
        string Email,
        string Token,
        string NewPassword
    );
}
