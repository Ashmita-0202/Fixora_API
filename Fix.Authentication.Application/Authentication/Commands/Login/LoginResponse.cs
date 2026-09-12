using Fix.Shared.Domain.Models.Entities;

namespace Fix.Authentication.Application.Authentication.Commands.Login
{
    public class LoginCommandResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public UserRole UserRole { get; set; }
        public string Token { get; set; } = default!;

        // Only populated when UserRole == Technician
        public string? TechnicianApprovalStatus { get; set; }
        public bool? TechnicianIsActive { get; set; }
    }
}
