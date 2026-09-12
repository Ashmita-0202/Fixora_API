using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Commands.ToggleServiceCategoryStatus
{
    // Flips IsActive - used by Admin to show/hide a service from customers
    // without permanently deleting it (e.g. seasonal services, temporarily unavailable).
    public record ToggleServiceCategoryStatusCommand(string Id) : IRequest<Result<ToggleServiceCategoryStatusResponse>>;
}
