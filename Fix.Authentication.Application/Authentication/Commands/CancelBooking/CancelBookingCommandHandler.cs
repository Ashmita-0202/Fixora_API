using Fix.Authentication.Application.Authentication.Common;
using Fix.Authentication.Application.ServiceInterface;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Commands.CancelBooking
{
    public class CancelBookingCommandHandler : IRequestHandler<CancelBookingCommand, Result<BookingResponse>>
    {
        private readonly IBookingServices _bookingServices;

        public CancelBookingCommandHandler(IBookingServices bookingServices)
        {
            _bookingServices = bookingServices;
        }

        public async Task<Result<BookingResponse>> Handle(CancelBookingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _bookingServices.Cancel(request.BookingId, request.CustomerId);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while cancelling the booking", ex);
            }
        }
    }
}
