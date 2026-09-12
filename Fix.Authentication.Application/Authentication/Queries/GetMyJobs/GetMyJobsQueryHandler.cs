using Fix.Authentication.Application.Authentication.Common;
using Fix.Authentication.Application.ServiceInterface;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Queries.GetMyJobs
{
    public class GetMyJobsQueryHandler : IRequestHandler<GetMyJobsQuery, Result<List<BookingResponse>>>
    {
        private readonly IBookingServices _bookingServices;

        public GetMyJobsQueryHandler(IBookingServices bookingServices)
        {
            _bookingServices = bookingServices;
        }

        public async Task<Result<List<BookingResponse>>> Handle(GetMyJobsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _bookingServices.GetMyJobs(request.TechnicianId, request.Status);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching your jobs", ex);
            }
        }
    }
}
