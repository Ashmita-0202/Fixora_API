namespace Fix.Authentication.Application.Authentication.Common
{
    public class BookingResponse
    {
        public string Id { get; set; } = default!;

        public string ServiceCategoryId { get; set; } = default!;
        public string ServiceName { get; set; } = default!;

        public string CustomerId { get; set; } = default!;
        public string CustomerName { get; set; } = default!;

        public string? TechnicianId { get; set; }
        public string? TechnicianName { get; set; }

        public DateTime ScheduledDate { get; set; }
        public string ScheduledTime { get; set; } = default!;
        public string Address { get; set; } = default!;
        public decimal Amount { get; set; }
        public string? Notes { get; set; }

        public string Status { get; set; } = default!;

        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
