using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Fix.Authentication.Application.ServiceInterface;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Queries.AllUser
{
    public class GetAllServiceCategoriesQueryHandler:IRequestHandler<GetAllServiceCategoriesQuery, Result<List<GetAllServiceCategoriesResponse>>>
    {
        public readonly IServiceCategories _serviceCategories;
        public readonly IMapper _mapper;

        public GetAllServiceCategoriesQueryHandler(IMapper mapper,IServiceCategories serviceCategories)
        {

            _serviceCategories = serviceCategories;
            _mapper = mapper;
        }
        public async Task<Result<List<GetAllServiceCategoriesResponse>>> Handle(GetAllServiceCategoriesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var allCategories = await _serviceCategories.GetAllServiceCategory(request.ActiveOnly, cancellationToken);
                return allCategories;
            }
            catch (Exception ex)
            {

                throw new Exception("An error occured while fetching all service Categories ", ex);
            }
        }
    }
}
