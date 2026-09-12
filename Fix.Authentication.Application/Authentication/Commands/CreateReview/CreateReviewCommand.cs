using Fix.Authentication.Application.Authentication.Common;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Commands.CreateReview
{
    public record CreateReviewCommand(string CustomerId, string BookingId, int Rating, string? Comment) : IRequest<Result<ReviewResponse>>;
}
