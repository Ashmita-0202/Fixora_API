namespace Fix.Authentication.Application.ServiceInterface
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Verify(string password, string passwordHash);
    }
}
