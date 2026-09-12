using Fix.Authentication.Application.Authentication.Common;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Commands.CreateBooking
{
    public record CreateBookingCommand
    (
        string CustomerId,   // taken from the authenticated user's JWT, not the request body
        string ServiceCategoryId,
        DateTime ScheduledDate,
        string ScheduledTime,
        string Address,
        string? Notes
    ) : IRequest<Result<BookingResponse>>;
}
