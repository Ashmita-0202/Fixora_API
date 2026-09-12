namespace Fix.Authentication.Application.Authentication.Common
{
    public class TechnicianResponse
    {
        public string Id { get; set; } = default!;              // TechnicianProfile id
        public string UserId { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string Specialty { get; set; } = default!;
        public string City { get; set; } = default!;
        public int ExperienceYears { get; set; }
        public string? Certification { get; set; }

        public string ApprovalStatus { get; set; } = default!;
        public bool IsActive { get; set; }
        public string? RejectionReason { get; set; }

        public int JobsCompleted { get; set; }
        public double AverageRating { get; set; }
        public int ReviewCount { get; set; }

        public DateTime AppliedAt { get; set; }
        public DateTime? ReviewedAt { get; set; }
    }

    public class EarningsResponse
    {
        public decimal TotalEarnings { get; set; }
        public decimal ThisMonthEarnings { get; set; }
        public int JobsCompleted { get; set; }
        public List<EarningsJobItem> RecentJobs { get; set; } = new();
    }

    public class EarningsJobItem
    {
        public string BookingId { get; set; } = default!;
        public string ServiceName { get; set; } = default!;
        public decimal Amount { get; set; }
        public DateTime CompletedAt { get; set; }
    }
}
