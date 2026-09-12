using Fix.Authentication.Application.ServiceInterface;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Commands.UpdateServiceCategory
{
    public class UpdateServiceCategoryCommandHandler : IRequestHandler<UpdateServiceCategoryCommand, Result<UpdateServiceCategoryResponse>>
    {
        private readonly IServiceCategories _serviceCategories;

        public UpdateServiceCategoryCommandHandler(IServiceCategories serviceCategories)
        {
            _serviceCategories = serviceCategories;
        }

        public async Task<Result<UpdateServiceCategoryResponse>> Handle(UpdateServiceCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _serviceCategories.Update(request);

                if (result.Errors.Any())
                {
                    var errors = string.Join(", ", result.Errors);
                    return Result<UpdateServiceCategoryResponse>.Failure(errors);
                }

                if (result is null || !result.IsSuccess)
                {
                    return Result<UpdateServiceCategoryResponse>.Failure("Unable to update service category.");
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating service category", ex);
            }
        }
    }
}
