using Fix.Authentication.Application.Authentication.Common;
using Fix.Shared.Domain.Abstractions;

namespace Fix.Authentication.Application.ServiceInterface
{
    public interface IReviewServices
    {
        Task<Result<ReviewResponse>> Create(string customerId, string bookingId, int rating, string? comment);

        Task<Result<List<ReviewResponse>>> GetForTechnician(string technicianId);

        // Average rating + count for a technician - used by TechnicianServices to populate TechnicianResponse.
        Task<(double AverageRating, int ReviewCount)> GetRatingSummary(string technicianId);
        Task<Dictionary<string, (double AverageRating, int ReviewCount)>> GetRatingSummaries(List<string> technicianIds);
    }
}
