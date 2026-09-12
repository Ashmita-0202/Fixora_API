namespace Fix.Shared.Domain.Models.Entities
{
    public enum TechnicianApprovalStatus
    {
        Pending = 0,
        Approved = 1,
        Rejected = 2
    }

    public class TechnicianProfile : Fix.Shared.Domain.Primitive.Entity
    {
        public TechnicianProfile(
            string id,
            string userId,
            string specialty,
            string city,
            int experienceYears,
            string? certification
            ) : base(id)
        {
            UserId = userId;
            Specialty = specialty;
            City = city;
            ExperienceYears = experienceYears;
            Certification = certification;
            ApprovalStatus = TechnicianApprovalStatus.Pending;
            IsActive = false; // only flips true once Admin approves
            AppliedAt = DateTime.UtcNow;
        }

        public string UserId { get; set; } = default!;
        public string Specialty { get; set; } = default!;
        public string City { get; set; } = default!;
        public int ExperienceYears { get; set; }
        public string? Certification { get; set; }

        public TechnicianApprovalStatus ApprovalStatus { get; set; }
        public bool IsActive { get; set; }
        public string? RejectionReason { get; set; }

        public DateTime AppliedAt { get; set; }
        public DateTime? ReviewedAt { get; set; }
    }
}
