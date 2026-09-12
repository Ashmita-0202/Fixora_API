namespace Fix.Shared.Domain.Models.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
         
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
