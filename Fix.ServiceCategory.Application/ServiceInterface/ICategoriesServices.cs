using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fix.Categories.Application.Categories.Commands.AddCategories;
using Fix.Shared.Domain.Abstractions;

namespace Fix.Categories.Application.ServiceInterface
{
    public interface ICategoriesServices
    {
        Task<Result<AddCategoriesResponse>> Add(AddCategoriesCommand request);
    }
}
