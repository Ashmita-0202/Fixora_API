using Fix.Authentication.Application.ServiceInterface;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Commands.ToggleServiceCategoryStatus
{
    public class ToggleServiceCategoryStatusCommandHandler : IRequestHandler<ToggleServiceCategoryStatusCommand, Result<ToggleServiceCategoryStatusResponse>>
    {
        private readonly IServiceCategories _serviceCategories;

        public ToggleServiceCategoryStatusCommandHandler(IServiceCategories serviceCategories)
        {
            _serviceCategories = serviceCategories;
        }

        public async Task<Result<ToggleServiceCategoryStatusResponse>> Handle(ToggleServiceCategoryStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _serviceCategories.ToggleStatus(request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while toggling service category status", ex);
            }
        }
    }
}
