using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Fix.Categories.Application.ServiceInterface;
using Fix.Shared.Domain.Abstractions;
using FluentValidation;
using MediatR;

namespace Fix.Categories.Application.Categories.Commands.AddCategories
{
    public class AddCategoriesRequestHandler : IRequestHandler<AddCategoriesCommand, Result<AddCategoriesResponse>>
    {
        public readonly ICategoriesServices _categoryServices;

        public readonly IValidator<AddCategoriesCommand> _validator;
        public readonly IMapper _mapper;
        public AddCategoriesRequestHandler(ICategoriesServices categoryServices, IMapper mapper, IValidator<AddCategoriesCommand> validator)
        {
            _categoryServices = categoryServices;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<Result<AddCategoriesResponse>> Handle(AddCategoriesCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var addcategories = await _categoryServices.Add(request);

                if (addcategories.Errors.Any())
                {
                    var errors = string.Join(", ", addcategories.Errors);
                    return Result<AddCategoriesResponse>.Failure(errors);
                }

                if (addcategories is null || !addcategories.IsSuccess)
                {
                    return Result<AddCategoriesResponse>.Failure("Unable to add user.");
                }

                return addcategories;

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding Servicecategories", ex);


            }
        }
    }

}
