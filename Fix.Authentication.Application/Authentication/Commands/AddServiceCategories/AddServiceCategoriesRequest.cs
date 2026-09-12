using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fix.Authentication.Application.Authentication.Commands.AddServiceCategories
{
    public record  AddServiceCategoriesRequest
   (
           string categoryName,
           string? description,
           string? iconUrl,
           decimal baseServiceCharge,
           string? estimatedDuration,
           bool isActive

    );
}
