using Fix.Authentication.Application.Authentication.Common;
using Fix.Authentication.Application.ServiceInterface;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Queries.GetBookingById
{
    public class GetBookingByIdQueryHandler : IRequestHandler<GetBookingByIdQuery, Result<BookingResponse>>
    {
        private readonly IBookingServices _bookingServices;

        public GetBookingByIdQueryHandler(IBookingServices bookingServices)
        {
            _bookingServices = bookingServices;
        }

        public async Task<Result<BookingResponse>> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _bookingServices.GetById(request.Id, request.RequesterId, request.RequesterRole);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching the booking", ex);
            }
        }
    }
}
