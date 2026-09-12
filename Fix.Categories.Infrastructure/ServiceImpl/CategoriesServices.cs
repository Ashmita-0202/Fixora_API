using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using Fix.Authentication.Application.Authentication.Commands.AddUser;
using Fix.Categories.Application.Categories.Commands.AddCategories;
using Fix.Categories.Application.ServiceInterface;
using Fix.Shared.Domain.Abstractions;
using Fix.Shared.Domain.IRepository;
using Fix.Shared.Domain.Models.Entities;

namespace Fix.Categories.Infrastructure.ServiceImpl
{
    public class CategoriesServices : ICategoriesServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoriesServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<AddCategoriesResponse>> Add(AddCategoriesCommand request)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var userData = new ServiceCategory
                    {
                        Id = Guid.NewGuid(),
                        CategoryName = request.categoryName,
                        Description = request.description,
                        IconUrl = request.iconUrl,
                        BaseServiceCharge = request.baseServiceCharge,
                        IsActive = request.isActive
                    };

                    await _unitOfWork.BaseRepository<ServiceCategory>().AddAsync(userData);
                    await _unitOfWork.SaveChangesAsync();
                    scope.Complete();

                    var resultDTOs = _mapper.Map<AddCategoriesResponse>(userData);
                    return Result<AddCategoriesResponse>.Success(resultDTOs);
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while adding category", ex);
                }
            }
        }
    }
}