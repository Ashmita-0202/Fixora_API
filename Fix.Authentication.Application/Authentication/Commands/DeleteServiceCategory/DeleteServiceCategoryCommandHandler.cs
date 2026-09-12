using Fix.Authentication.Application.ServiceInterface;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Commands.DeleteServiceCategory
{
    public class DeleteServiceCategoryCommandHandler : IRequestHandler<DeleteServiceCategoryCommand, Result<DeleteServiceCategoryResponse>>
    {
        private readonly IServiceCategories _serviceCategories;

        public DeleteServiceCategoryCommandHandler(IServiceCategories serviceCategories)
        {
            _serviceCategories = serviceCategories;
        }

        public async Task<Result<DeleteServiceCategoryResponse>> Handle(DeleteServiceCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _serviceCategories.Delete(request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting service category", ex);
            }
        }
    }
}
