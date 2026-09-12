using Fix.Authentication.Application.Authentication.Common;
using Fix.Authentication.Application.ServiceInterface;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Commands.AcceptJob
{
    public class AcceptJobCommandHandler : IRequestHandler<AcceptJobCommand, Result<BookingResponse>>
    {
        private readonly IBookingServices _bookingServices;

        public AcceptJobCommandHandler(IBookingServices bookingServices)
        {
            _bookingServices = bookingServices;
        }

        public async Task<Result<BookingResponse>> Handle(AcceptJobCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _bookingServices.AcceptJob(request.BookingId, request.TechnicianId);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while accepting the job", ex);
            }
        }
    }
}
