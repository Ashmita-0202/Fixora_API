namespace Fix.Authentication.Application.Authentication.Queries.AllUser
{
    public class GetAllServiceCategoriesResponse
    {
        public string Id { get; set; } = default!;
        public string CategoryName { get; set; } = default!;
        public string? Description { get; set; }
        public string? IconUrl { get; set; }
        public decimal BaseServiceCharge { get; set; }
        public string? EstimatedDuration { get; set; }
        public bool IsActive { get; set; }
    }
}
