using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Fix.Authentication.Application.Authentication.Commands.AddServiceCategories
{
    public static class AddServiceCategoriesRequestMapper
    {
        public static AddServiceCategoriesCommand ToCommand(this AddServiceCategoriesRequest request)
        {
            return new AddServiceCategoriesCommand
            (
                request.categoryName,
                request.description,
                request.iconUrl,
                request.baseServiceCharge,
                request.estimatedDuration,
                request.isActive
            );
        }
    }
}
