namespace Fix.Authentication.Application.Authentication.Commands.UpdateTechnicianProfile
{
    public static class UpdateTechnicianProfileRequestMapper
    {
        public static UpdateTechnicianProfileCommand ToCommand(this UpdateTechnicianProfileRequest request, string userId)
        {
            return new UpdateTechnicianProfileCommand(userId, request.Specialty, request.City, request.ExperienceYears, request.Certification);
        }
    }
}
