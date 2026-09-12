using Fix.Authentication.Application.Authentication.Common;
using Fix.Authentication.Application.ServiceInterface;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Queries.GetTechnicianReviews
{
    public class GetTechnicianReviewsQueryHandler : IRequestHandler<GetTechnicianReviewsQuery, Result<List<ReviewResponse>>>
    {
        private readonly IReviewServices _reviewServices;

        public GetTechnicianReviewsQueryHandler(IReviewServices reviewServices)
        {
            _reviewServices = reviewServices;
        }

        public async Task<Result<List<ReviewResponse>>> Handle(GetTechnicianReviewsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _reviewServices.GetForTechnician(request.TechnicianId);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching reviews", ex);
            }
        }
    }
}
