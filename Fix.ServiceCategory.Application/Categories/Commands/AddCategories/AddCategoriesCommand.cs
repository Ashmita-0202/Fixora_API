using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Categories.Application.Categories.Commands.AddCategories
{
    public record AddCategoriesCommand
    (
           string categoryName,
           string? description,
           string? iconUrl,
           decimal baseServiceCharge,
           bool isActive


    ) : IRequest<Result<AddCategoriesResponse>>;
}
