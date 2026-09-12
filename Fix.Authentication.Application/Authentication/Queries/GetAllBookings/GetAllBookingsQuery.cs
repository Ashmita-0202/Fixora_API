using Fix.Authentication.Application.Authentication.Common;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Queries.GetAllBookings
{
    public record GetAllBookingsQuery(string? Status) : IRequest<Result<List<BookingResponse>>>;
}
