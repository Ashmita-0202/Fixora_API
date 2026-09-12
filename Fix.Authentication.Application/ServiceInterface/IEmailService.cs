namespace Fix.Authentication.Application.ServiceInterface
{
    public interface IEmailService
    {
        Task SendPasswordResetEmailAsync(string toEmail, string resetToken);
    }
}
