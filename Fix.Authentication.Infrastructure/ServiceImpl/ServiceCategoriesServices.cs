using System.Transactions;
using AutoMapper;
using Fix.Authentication.Application.Authentication.Commands.AddServiceCategories;
using Fix.Authentication.Application.Authentication.Commands.DeleteServiceCategory;
using Fix.Authentication.Application.Authentication.Commands.ToggleServiceCategoryStatus;
using Fix.Authentication.Application.Authentication.Commands.UpdateServiceCategory;
using Fix.Authentication.Application.Authentication.Queries.AllUser;
using Fix.Authentication.Application.ServiceInterface;
using Fix.Shared.Domain.Abstractions;
using Fix.Shared.Domain.IRepository;
using Fix.Shared.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fix.Authentication.Infrastructure.ServiceImpl
{
    public class ServiceCategoriesServices : IServiceCategories
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public ServiceCategoriesServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        public async Task<Result<AddServiceCategoriesResponse>> Add(AddServiceCategoriesCommand request)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var nameExists = await _unitOfWork.BaseRepository<ServiceCategory>()
                        .AnyAsync(c => c.CategoryName.ToLower() == request.categoryName.ToLower());

                    if (nameExists)
                    {
                        return Result<AddServiceCategoriesResponse>.Failure("Conflict: A service with this name already exists.");
                    }

                    string newId = Guid.NewGuid().ToString();
                    var categoryData = new ServiceCategory
                    (
                       newId,
                        request.categoryName,
                       request.description,
                       request.iconUrl,
                       request.baseServiceCharge,
                       request.estimatedDuration,
                       request.isActive
                    );

                    await _unitOfWork.BaseRepository<ServiceCategory>().AddAsync(categoryData);
                    await _unitOfWork.SaveChangesAsync();
                    scope.Complete();

                    var resultDTOs = _mapper.Map<AddServiceCategoriesResponse>(categoryData);
                    return Result<AddServiceCategoriesResponse>.Success(resultDTOs);

                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw new Exception("An error occurred while adding service Category ", ex);

                }
            }
        }

        public async Task<Result<List<GetAllServiceCategoriesResponse>>> GetAllServiceCategory(bool? activeOnly, CancellationToken cancellationToken = default)
        {
            try
            {
                var query = await _unitOfWork.BaseRepository<ServiceCategory>().GetAllAsync();

                if (activeOnly == true)
                {
                    query = query.Where(c => c.IsActive);
                }

                var serviceCategories = await query
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

                var response = _mapper.Map<List<GetAllServiceCategoriesResponse>>(serviceCategories);

                return Result<List<GetAllServiceCategoriesResponse>>.Success(response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching all service categories", ex);
            }
        }

        public async Task<Result<GetAllServiceCategoriesResponse>> GetById(string id, CancellationToken cancellationToken = default)
        {
            try
            {
                var category = await _unitOfWork.BaseRepository<ServiceCategory>()
                    .GetSingleAsync(c => c.Id == id);

                if (category is null)
                {
                    return Result<GetAllServiceCategoriesResponse>.Failure("NotFound: Service category not found.");
                }

                var response = _mapper.Map<GetAllServiceCategoriesResponse>(category);
                return Result<GetAllServiceCategoriesResponse>.Success(response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching the service category", ex);
            }
        }

        public async Task<Result<UpdateServiceCategoryResponse>> Update(UpdateServiceCategoryCommand request)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var category = await _unitOfWork.BaseRepository<ServiceCategory>()
                        .GetSingleAsync(c => c.Id == request.Id);

                    if (category is null)
                    {
                        return Result<UpdateServiceCategoryResponse>.Failure("NotFound: Service category not found.");
                    }

                    var nameTakenByAnother = await _unitOfWork.BaseRepository<ServiceCategory>()
                        .AnyAsync(c => c.Id != request.Id && c.CategoryName.ToLower() == request.CategoryName.ToLower());

                    if (nameTakenByAnother)
                    {
                        return Result<UpdateServiceCategoryResponse>.Failure("Conflict: A service with this name already exists.");
                    }

                    category.CategoryName = request.CategoryName;
                    category.Description = request.Description;
                    category.IconUrl = request.IconUrl;
                    category.BaseServiceCharge = request.BaseServiceCharge;
                    category.EstimatedDuration = request.EstimatedDuration;
                    category.IsActive = request.IsActive;

                    _unitOfWork.BaseRepository<ServiceCategory>().Update(category);
                    await _unitOfWork.SaveChangesAsync();
                    scope.Complete();

                    var response = _mapper.Map<UpdateServiceCategoryResponse>(category);
                    return Result<UpdateServiceCategoryResponse>.Success(response);
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw new Exception("An error occurred while updating service category", ex);
                }
            }
        }

        public async Task<Result<ToggleServiceCategoryStatusResponse>> ToggleStatus(string id)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var category = await _unitOfWork.BaseRepository<ServiceCategory>()
                        .GetSingleAsync(c => c.Id == id);

                    if (category is null)
                    {
                        return Result<ToggleServiceCategoryStatusResponse>.Failure("NotFound: Service category not found.");
                    }

                    category.IsActive = !category.IsActive;

                    _unitOfWork.BaseRepository<ServiceCategory>().Update(category);
                    await _unitOfWork.SaveChangesAsync();
                    scope.Complete();

                    return Result<ToggleServiceCategoryStatusResponse>.Success(
                        new ToggleServiceCategoryStatusResponse { Id = category.Id, IsActive = category.IsActive });
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw new Exception("An error occurred while toggling service category status", ex);
                }
            }
        }

        public async Task<Result<DeleteServiceCategoryResponse>> Delete(string id)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var category = await _unitOfWork.BaseRepository<ServiceCategory>()
                        .GetSingleAsync(c => c.Id == id);

                    if (category is null)
                    {
                        return Result<DeleteServiceCategoryResponse>.Failure("NotFound: Service category not found.");
                    }

                    // NOTE: once the Booking module exists, this should check for
                    // existing bookings referencing this category and block/soft-delete
                    // instead of hard-deleting, to avoid orphaning booking records.
                    _unitOfWork.BaseRepository<ServiceCategory>().Delete(category);
                    await _unitOfWork.SaveChangesAsync();
                    scope.Complete();

                    return Result<DeleteServiceCategoryResponse>.Success(
                        new DeleteServiceCategoryResponse { Message = "Service category deleted successfully." });
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw new Exception("An error occurred while deleting service category", ex);
                }
            }
        }
    }
}
