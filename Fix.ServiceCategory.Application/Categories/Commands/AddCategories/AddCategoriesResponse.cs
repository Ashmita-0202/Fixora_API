using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fix.Categories.Application.Categories.Commands.AddCategories
{
    public record AddCategoriesResponse
   (
             string id,
           string categoryName,
           string? description,
           string? iconUrl,
           decimal baseServiceCharge,
           bool isActive

   );
}
