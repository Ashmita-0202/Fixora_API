using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Commands.DeleteServiceCategory
{
    public record DeleteServiceCategoryCommand(string Id) : IRequest<Result<DeleteServiceCategoryResponse>>;
}
