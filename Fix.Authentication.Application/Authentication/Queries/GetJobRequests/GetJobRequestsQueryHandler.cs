using Fix.Authentication.Application.Authentication.Common;
using Fix.Authentication.Application.ServiceInterface;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Queries.GetJobRequests
{
    public class GetJobRequestsQueryHandler : IRequestHandler<GetJobRequestsQuery, Result<List<BookingResponse>>>
    {
        private readonly IBookingServices _bookingServices;

        public GetJobRequestsQueryHandler(IBookingServices bookingServices)
        {
            _bookingServices = bookingServices;
        }

        public async Task<Result<List<BookingResponse>>> Handle(GetJobRequestsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _bookingServices.GetJobRequests();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching job requests", ex);
            }
        }
    }
}
