using Fix.Authentication.Application.Authentication.Common;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Queries.GetBookingById
{
    public record GetBookingByIdQuery(string Id, string RequesterId, string RequesterRole) : IRequest<Result<BookingResponse>>;
}
