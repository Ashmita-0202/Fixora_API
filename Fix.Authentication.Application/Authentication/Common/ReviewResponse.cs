namespace Fix.Authentication.Application.Authentication.Common
{
    public class ReviewResponse
    {
        public string Id { get; set; } = default!;
        public string BookingId { get; set; } = default!;
        public string CustomerId { get; set; } = default!;
        public string CustomerName { get; set; } = default!;
        public string TechnicianId { get; set; } = default!;
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
