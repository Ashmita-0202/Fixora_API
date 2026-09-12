namespace Fix.Authentication.Application.Authentication.Common
{
    public class MyProfileResponse
    {
        public string Id { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string UserRole { get; set; } = default!;
        public DateTime? CreatedAt { get; set; }
    }
}
