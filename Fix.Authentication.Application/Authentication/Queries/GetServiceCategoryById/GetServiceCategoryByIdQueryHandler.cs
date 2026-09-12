using Fix.Authentication.Application.Authentication.Queries.AllUser;
using Fix.Authentication.Application.ServiceInterface;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Queries.GetServiceCategoryById
{
    public class GetServiceCategoryByIdQueryHandler : IRequestHandler<GetServiceCategoryByIdQuery, Result<GetAllServiceCategoriesResponse>>
    {
        private readonly IServiceCategories _serviceCategories;

        public GetServiceCategoryByIdQueryHandler(IServiceCategories serviceCategories)
        {
            _serviceCategories = serviceCategories;
        }

        public async Task<Result<GetAllServiceCategoriesResponse>> Handle(GetServiceCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _serviceCategories.GetById(request.Id, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching the service category", ex);
            }
        }
    }
}
