using FluentValidation;

namespace Fix.Authentication.Application.Authentication.Commands.MarkNotificationAsRead
{
    public class MarkNotificationAsReadCommandValidator : AbstractValidator<MarkNotificationAsReadCommand>
    {
        public MarkNotificationAsReadCommandValidator()
        {
            RuleFor(x => x.NotificationId).NotEmpty().WithMessage("Notification id is required.");
        }
    }
}
