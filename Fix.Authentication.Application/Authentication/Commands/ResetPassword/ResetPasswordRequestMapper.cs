namespace Fix.Authentication.Application.Authentication.Commands.ResetPassword
{
    public static class ResetPasswordRequestMapper
    {
        public static ResetPasswordCommand ToCommand(this ResetPasswordRequest request)
        {
            return new ResetPasswordCommand
            (
                request.Email,
                request.Token,
                request.NewPassword
            );
        }
    }
}
