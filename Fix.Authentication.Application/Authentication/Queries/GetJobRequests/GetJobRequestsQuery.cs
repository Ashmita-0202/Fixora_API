using Fix.Authentication.Application.Authentication.Common;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Queries.GetJobRequests
{
    public record GetJobRequestsQuery() : IRequest<Result<List<BookingResponse>>>;
}
