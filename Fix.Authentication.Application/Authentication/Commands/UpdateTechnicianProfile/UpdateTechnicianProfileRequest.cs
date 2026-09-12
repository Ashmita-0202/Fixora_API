namespace Fix.Authentication.Application.Authentication.Commands.UpdateTechnicianProfile
{
    public record UpdateTechnicianProfileRequest(string Specialty, string City, int ExperienceYears, string? Certification);
}
