using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Fix.Categories.Application.Categories.Commands.AddCategories;
using Fix.Shared.Domain.Models.Entities;

namespace Fix.Categories.Application.AutoMapper
{
    public class MappingProfile:Profile
    {
       
            public MappingProfile()
            {

            #region categoryServices
            #region categoryServices
            CreateMap<Fix.Shared.Domain.Models.Entities.ServiceCategory, AddCategoriesResponse>();
            CreateMap<AddCategoriesCommand, Fix.Shared.Domain.Models.Entities.ServiceCategory>();
            #endregion
            #endregion
        }


    }
}
