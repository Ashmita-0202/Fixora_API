namespace Fix.Authentication.Application.Authentication.Commands.UpdateProfile
{
    public static class UpdateProfileRequestMapper
    {
        public static UpdateProfileCommand ToCommand(this UpdateProfileRequest request, string userId)
        {
            return new UpdateProfileCommand(userId, request.FirstName, request.LastName, request.Phone, request.Address);
        }
    }
}
