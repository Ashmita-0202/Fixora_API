namespace Fix.Shared.Domain.Models.Entities
{
    public class Review : Fix.Shared.Domain.Primitive.Entity
    {
        public Review(
            string id,
            string bookingId,
            string customerId,
            string technicianId,
            int rating,
            string? comment
            ) : base(id)
        {
            BookingId = bookingId;
            CustomerId = customerId;
            TechnicianId = technicianId;
            Rating = rating;
            Comment = comment;
            CreatedAt = DateTime.UtcNow;
        }

        public string BookingId { get; set; } = default!;   // one review per booking
        public string CustomerId { get; set; } = default!;
        public string TechnicianId { get; set; } = default!;
        public int Rating { get; set; }                     // 1-5
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
