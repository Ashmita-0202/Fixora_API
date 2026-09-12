using Fix.Authentication.Application.Authentication.Queries.AllUser;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Queries.GetServiceCategoryById
{
    public record GetServiceCategoryByIdQuery(string Id) : IRequest<Result<GetAllServiceCategoriesResponse>>;
}
