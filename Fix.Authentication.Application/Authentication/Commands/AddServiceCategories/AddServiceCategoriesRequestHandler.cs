using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Fix.Authentication.Application.Authentication.Commands.AddUser;
using Fix.Authentication.Application.ServiceInterface;
using Fix.Shared.Domain.Abstractions;
using Fix.Shared.Domain.IRepository;
using FluentValidation;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Commands.AddServiceCategories
{
    public  class AddServiceCategoriesRequestHandler: IRequestHandler<AddServiceCategoriesCommand, Result<AddServiceCategoriesResponse>>
    {
        public readonly IServiceCategories _serviceCategories;
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;
        public AddServiceCategoriesRequestHandler(IServiceCategories serviceCategories,IUnitOfWork unitOfWork,IMapper mapper)
        {
            _serviceCategories = serviceCategories;
            _unitOfWork = unitOfWork;
            _mapper = mapper;


        }
        public async Task<Result<AddServiceCategoriesResponse>> Handle(AddServiceCategoriesCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var addCategories = await _serviceCategories.Add(request);

                if (addCategories.Errors.Any())
                {
                    var errors = string.Join(", ", addCategories.Errors);
                    return Result<AddServiceCategoriesResponse>.Failure(errors);
                }

                if (addCategories is null || !addCategories.IsSuccess)
                {
                    return Result<AddServiceCategoriesResponse>.Failure("Unable to add service Categories.");
                }

                return addCategories;

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding categories", ex);


            }
        }
    }
}
