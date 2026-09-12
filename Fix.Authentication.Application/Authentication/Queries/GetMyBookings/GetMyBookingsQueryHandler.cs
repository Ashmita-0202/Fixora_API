using Fix.Authentication.Application.Authentication.Common;
using Fix.Authentication.Application.ServiceInterface;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Queries.GetMyBookings
{
    public class GetMyBookingsQueryHandler : IRequestHandler<GetMyBookingsQuery, Result<List<BookingResponse>>>
    {
        private readonly IBookingServices _bookingServices;

        public GetMyBookingsQueryHandler(IBookingServices bookingServices)
        {
            _bookingServices = bookingServices;
        }

        public async Task<Result<List<BookingResponse>>> Handle(GetMyBookingsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _bookingServices.GetMyBookings(request.CustomerId, request.Status);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching your bookings", ex);
            }
        }
    }
}
