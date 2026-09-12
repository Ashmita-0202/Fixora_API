using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fix.Categories.Application.Categories.Commands.AddCategories
{
    public static class AddCategoriesRequestMapper
    {

        public static AddCategoriesCommand ToCommand(this AddCategoriesRequest request)
        {
            return new AddCategoriesCommand
                (


                     request.categoryName,
                     request.description,
                        request.iconUrl,
                        request.baseServiceCharge,
                        request.isActive


                );
        }
    }
}
