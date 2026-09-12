using Fix.Authentication.Application.Authentication.Common;
using Fix.Shared.Domain.Abstractions;

namespace Fix.Authentication.Application.ServiceInterface
{
    public interface ITechnicianServices
    {
        Task<Result<List<TechnicianResponse>>> GetPendingApplications();

        Task<Result<List<TechnicianResponse>>> GetAll(bool? activeOnly);

        Task<Result<TechnicianResponse>> GetByUserId(string userId);

        Task<Result<TechnicianResponse>> Approve(string profileId);

        Task<Result<TechnicianResponse>> Reject(string profileId, string? reason);

        Task<Result<TechnicianResponse>> ToggleActiveStatus(string profileId);

        Task<Result<TechnicianResponse>> UpdateProfile(string userId, string specialty, string city, int experienceYears, string? certification);

        Task<Result<EarningsResponse>> GetEarnings(string userId);
    }
}
