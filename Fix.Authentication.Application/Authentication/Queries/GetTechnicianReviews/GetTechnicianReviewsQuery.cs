using Fix.Authentication.Application.Authentication.Common;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Queries.GetTechnicianReviews
{
    public record GetTechnicianReviewsQuery(string TechnicianId) : IRequest<Result<List<ReviewResponse>>>;
}
