using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Commands.AddServiceCategories
{
    public record  AddServiceCategoriesCommand
    (

           string categoryName,
           string? description,
           string? iconUrl,
           decimal baseServiceCharge,
           string? estimatedDuration,
           bool isActive

    ) :IRequest<Result<AddServiceCategoriesResponse>>;
}
