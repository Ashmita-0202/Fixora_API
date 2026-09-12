namespace Fix.Authentication.Application.Authentication.Commands.ForgotPassword
{
    public static class ForgotPasswordRequestMapper
    {
        public static ForgotPasswordCommand ToCommand(this ForgotPasswordRequest request)
        {
            return new ForgotPasswordCommand
            (
                request.Email
            );
        }
    }
}
