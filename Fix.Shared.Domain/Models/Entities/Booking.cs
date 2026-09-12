namespace Fix.Shared.Domain.Models.Entities
{
    public enum BookingStatus
    {
        Pending = 0,     // just created, waiting for a technician to accept
        Scheduled = 1,   // a technician accepted, job is scheduled
        InProgress = 2,  // technician has started the job
        Completed = 3,
        Cancelled = 4
    }

    public class Booking : Fix.Shared.Domain.Primitive.Entity
    {
        public Booking(
            string id,
            string customerId,
            string serviceCategoryId,
            DateTime scheduledDate,
            string scheduledTime,
            string address,
            decimal amount,
            string? notes
            ) : base(id)
        {
            CustomerId = customerId;
            ServiceCategoryId = serviceCategoryId;
            TechnicianId = null;
            ScheduledDate = scheduledDate;
            ScheduledTime = scheduledTime;
            Address = address;
            Amount = amount;
            Notes = notes;
            Status = BookingStatus.Pending;
            CreatedAt = DateTime.UtcNow;
        }

        public string CustomerId { get; set; } = default!;
        public string ServiceCategoryId { get; set; } = default!;
        public string? TechnicianId { get; set; }

        public DateTime ScheduledDate { get; set; }
        public string ScheduledTime { get; set; } = default!;
        public string Address { get; set; } = default!;
        public decimal Amount { get; set; }
        public string? Notes { get; set; }

        public BookingStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
