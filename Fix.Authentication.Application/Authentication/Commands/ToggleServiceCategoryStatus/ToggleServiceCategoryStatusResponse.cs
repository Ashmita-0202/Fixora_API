namespace Fix.Authentication.Application.Authentication.Commands.ToggleServiceCategoryStatus
{
    public class ToggleServiceCategoryStatusResponse
    {
        public string Id { get; set; } = default!;
        public bool IsActive { get; set; }
    }
}
