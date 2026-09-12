using Fix.Authentication.Application.Authentication.Common;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Queries.GetMyBookings
{
    public record GetMyBookingsQuery(string CustomerId, string? Status) : IRequest<Result<List<BookingResponse>>>;
}
