using Fix.Authentication.Application.Authentication.Commands.AddServiceCategories;
using Fix.Authentication.Application.Authentication.Commands.DeleteServiceCategory;
using Fix.Authentication.Application.Authentication.Commands.ToggleServiceCategoryStatus;
using Fix.Authentication.Application.Authentication.Commands.UpdateServiceCategory;
using Fix.Authentication.Application.Authentication.Queries.AllUser;
using Fix.Shared.Domain.Abstractions;

namespace Fix.Authentication.Application.ServiceInterface
{
    public interface IServiceCategories
    {
        Task<Result<AddServiceCategoriesResponse>> Add(AddServiceCategoriesCommand request);
        Task<Result<List<GetAllServiceCategoriesResponse>>> GetAllServiceCategory(bool? activeOnly, CancellationToken cancellationToken = default);
        Task<Result<GetAllServiceCategoriesResponse>> GetById(string id, CancellationToken cancellationToken = default);
        Task<Result<UpdateServiceCategoryResponse>> Update(UpdateServiceCategoryCommand request);
        Task<Result<ToggleServiceCategoryStatusResponse>> ToggleStatus(string id);
        Task<Result<DeleteServiceCategoryResponse>> Delete(string id);
    }
}
