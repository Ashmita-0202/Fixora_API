using Fix.Authentication.Application.Authentication.Common;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Queries.GetMyEarnings
{
    public record GetMyEarningsQuery(string UserId) : IRequest<Result<EarningsResponse>>;
}
