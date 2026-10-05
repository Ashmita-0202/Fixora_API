namespace FIXORA.API.Controllers.Area
{
    public record AddTechnicianRequest(
        string FirstName,
        string LastName,
        string Address,
        string Phone,
        string Email,
        string Password,
        string Specialty,
        string City,
        int ExperienceYears,
        string? Certification);
}
