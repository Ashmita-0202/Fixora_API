using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Queries.AllUser
{
    // activeOnly = true  -> customer-facing browse (only services currently offered)
    // activeOnly = null  -> admin view (everything, including deactivated services)
    public record GetAllServiceCategoriesQuery(bool? ActiveOnly = null) : IRequest<Result<List<GetAllServiceCategoriesResponse>>>;
}
