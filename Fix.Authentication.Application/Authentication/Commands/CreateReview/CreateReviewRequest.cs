namespace Fix.Authentication.Application.Authentication.Commands.CreateReview
{
    public record CreateReviewRequest(string BookingId, int Rating, string? Comment);
}
