using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Fix.Authentication.Application.Authentication.Commands.AddServiceCategories;
using Fix.Authentication.Application.Authentication.Commands.AddUser;
using Fix.Authentication.Application.Authentication.Commands.UpdateServiceCategory;
using Fix.Authentication.Application.Authentication.Queries.AllUser;
using Fix.Shared.Domain.Models.Entities;

namespace Fix.Authentication.Application.Automapper
{

    public class MappingProfile:Profile
    {
        public MappingProfile()
        {

            #region UserServices
            CreateMap<User, AddUserCommandResponse>();
            CreateMap<AddUserCommand, User>();
            #endregion

            #region ServiceCategories
            CreateMap<ServiceCategory, AddServiceCategoriesResponse>()
                .ConstructUsing(s => new AddServiceCategoriesResponse(s.Id, s.CategoryName, s.Description, s.IconUrl, s.BaseServiceCharge, s.EstimatedDuration, s.IsActive));
            CreateMap<AddServiceCategoriesCommand, ServiceCategory>();
            CreateMap<ServiceCategory, GetAllServiceCategoriesResponse>();
            CreateMap<ServiceCategory, UpdateServiceCategoryResponse>();
            #endregion
        }

    }
}