namespace Fix.Authentication.Application.Authentication.Commands.ChangePassword
{
    public static class ChangePasswordRequestMapper
    {
        public static ChangePasswordCommand ToCommand(this ChangePasswordRequest request, string userId)
        {
            return new ChangePasswordCommand(userId, request.CurrentPassword, request.NewPassword);
        }
    }
}
