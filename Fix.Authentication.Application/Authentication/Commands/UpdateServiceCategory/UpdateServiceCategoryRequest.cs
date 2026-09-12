namespace Fix.Authentication.Application.Authentication.Commands.UpdateServiceCategory
{
    public record UpdateServiceCategoryRequest
    (
        string CategoryName,
        string? Description,
        string? IconUrl,
        decimal BaseServiceCharge,
        string? EstimatedDuration,
        bool IsActive
    );
}
