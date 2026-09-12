namespace Fix.Shared.Domain.Models.Entities
{
    public class Notification : Fix.Shared.Domain.Primitive.Entity
    {
        public Notification(
            string id,
            string userId,
            string type,
            string title,
            string body
            ) : base(id)
        {
            UserId = userId;
            Type = type;
            Title = title;
            Body = body;
            IsRead = false;
            CreatedAt = DateTime.UtcNow;
        }

        public string UserId { get; set; } = default!;
        public string Type { get; set; } = default!;    // e.g. "info", "success", "warning"
        public string Title { get; set; } = default!;
        public string Body { get; set; } = default!;
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
