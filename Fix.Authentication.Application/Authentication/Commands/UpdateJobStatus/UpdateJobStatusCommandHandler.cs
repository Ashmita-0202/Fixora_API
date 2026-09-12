using Fix.Authentication.Application.Authentication.Common;
using Fix.Authentication.Application.ServiceInterface;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Commands.UpdateJobStatus
{
    public class UpdateJobStatusCommandHandler : IRequestHandler<UpdateJobStatusCommand, Result<BookingResponse>>
    {
        private readonly IBookingServices _bookingServices;

        public UpdateJobStatusCommandHandler(IBookingServices bookingServices)
        {
            _bookingServices = bookingServices;
        }

        public async Task<Result<BookingResponse>> Handle(UpdateJobStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _bookingServices.UpdateJobStatus(request.BookingId, request.TechnicianId, request.NewStatus);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating job status", ex);
            }
        }
    }
}
