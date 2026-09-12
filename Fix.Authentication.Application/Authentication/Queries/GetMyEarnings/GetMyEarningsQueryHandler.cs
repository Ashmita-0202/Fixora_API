using Fix.Authentication.Application.Authentication.Common;
using Fix.Authentication.Application.ServiceInterface;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Queries.GetMyEarnings
{
    public class GetMyEarningsQueryHandler : IRequestHandler<GetMyEarningsQuery, Result<EarningsResponse>>
    {
        private readonly ITechnicianServices _technicianServices;

        public GetMyEarningsQueryHandler(ITechnicianServices technicianServices)
        {
            _technicianServices = technicianServices;
        }

        public async Task<Result<EarningsResponse>> Handle(GetMyEarningsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _technicianServices.GetEarnings(request.UserId);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching earnings", ex);
            }
        }
    }
}
