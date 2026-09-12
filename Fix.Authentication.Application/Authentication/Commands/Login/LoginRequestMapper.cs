namespace Fix.Authentication.Application.Authentication.Commands.Login
{
    public static class LoginRequestMapper
    {
        public static LoginCommand ToCommand(this LoginRequest request)
        {
            return new LoginCommand
            (
                request.Email,
                request.Password
            );
        }
    }
}
