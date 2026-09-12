namespace Fix.Authentication.Application.Authentication.Commands.UpdateProfile
{
    public record UpdateProfileRequest(string FirstName, string LastName, string Phone, string Address);
}
