namespace Fix.Authentication.Application.Authentication.Commands.CreateReview
{
    public static class CreateReviewRequestMapper
    {
        public static CreateReviewCommand ToCommand(this CreateReviewRequest request, string customerId)
        {
            return new CreateReviewCommand(customerId, request.BookingId, request.Rating, request.Comment);
        }
    }
}
