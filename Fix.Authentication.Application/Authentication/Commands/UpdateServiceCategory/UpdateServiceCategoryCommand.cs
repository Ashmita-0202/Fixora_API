using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Commands.UpdateServiceCategory
{
    public record UpdateServiceCategoryCommand
    (
        string Id,
        string CategoryName,
        string? Description,
        string? IconUrl,
        decimal BaseServiceCharge,
        string? EstimatedDuration,
        bool IsActive
    ) : IRequest<Result<UpdateServiceCategoryResponse>>;
}
