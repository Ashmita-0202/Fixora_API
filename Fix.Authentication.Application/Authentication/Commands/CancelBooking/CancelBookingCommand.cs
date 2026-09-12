using Fix.Authentication.Application.Authentication.Common;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Commands.CancelBooking
{
    public record CancelBookingCommand(string BookingId, string CustomerId) : IRequest<Result<BookingResponse>>;
}
