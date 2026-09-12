using Fix.Authentication.Application.ServiceInterface;
using Microsoft.Extensions.Logging;

namespace Fix.Authentication.Infrastructure.ServiceImpl
{
    // TODO: This is a placeholder implementation that logs instead of sending real email.
    // Swap this out for a real SMTP/SendGrid/etc. implementation before going to production.
    // No email provider decision was made yet, so this keeps the Forgot Password flow
    // fully functional (and testable via Swagger/logs) without blocking on SMTP setup.
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }

        public Task SendPasswordResetEmailAsync(string toEmail, string resetToken)
        {
            _logger.LogWarning(
                "[DEV EMAIL] Password reset requested for {Email}. Reset token: {Token} (this would normally be emailed as a link).",
                toEmail, resetToken);

            return Task.CompletedTask;
        }
    }
}
