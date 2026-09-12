using Fix.Shared.Domain.Models.Entities;

namespace Fix.Authentication.Application.ServiceInterface
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
