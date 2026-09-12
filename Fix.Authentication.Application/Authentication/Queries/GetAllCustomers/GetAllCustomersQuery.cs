using Fix.Authentication.Application.Authentication.Common;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Queries.GetAllCustomers
{
    public record GetAllCustomersQuery() : IRequest<Result<List<CustomerSummaryResponse>>>;
}
