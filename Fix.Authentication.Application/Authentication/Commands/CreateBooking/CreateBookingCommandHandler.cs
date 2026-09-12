using Fix.Authentication.Application.Authentication.Common;
using Fix.Authentication.Application.ServiceInterface;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Commands.CreateBooking
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, Result<BookingResponse>>
    {
        private readonly IBookingServices _bookingServices;

        public CreateBookingCommandHandler(IBookingServices bookingServices)
        {
            _bookingServices = bookingServices;
        }

        public async Task<Result<BookingResponse>> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _bookingServices.Create(
                    request.CustomerId,
                    request.ServiceCategoryId,
                    request.ScheduledDate,
                    request.ScheduledTime,
                    request.Address,
                    request.Notes);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the booking", ex);
            }
        }
    }
}
