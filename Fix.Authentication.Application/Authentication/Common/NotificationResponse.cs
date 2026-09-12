namespace Fix.Authentication.Application.Authentication.Common
{
    public class NotificationResponse
    {
        public string Id { get; set; } = default!;
        public string Type { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string Body { get; set; } = default!;
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
