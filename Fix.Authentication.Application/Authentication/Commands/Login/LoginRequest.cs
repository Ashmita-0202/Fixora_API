namespace Fix.Authentication.Application.Authentication.Commands.Login
{
    public record LoginRequest
    (
        string Email,
        string Password
    );
}
