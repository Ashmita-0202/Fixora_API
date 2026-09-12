using Fix.Authentication.Application.Authentication.Common;
using Fix.Authentication.Application.ServiceInterface;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Commands.CreateReview
{
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, Result<ReviewResponse>>
    {
        private readonly IReviewServices _reviewServices;

        public CreateReviewCommandHandler(IReviewServices reviewServices)
        {
            _reviewServices = reviewServices;
        }

        public async Task<Result<ReviewResponse>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return await _reviewServices.Create(request.CustomerId, request.BookingId, request.Rating, request.Comment);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while submitting the review", ex);
            }
        }
    }
}
