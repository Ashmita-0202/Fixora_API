using Fix.Authentication.Application.ServiceInterface;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Commands.ForgotPassword
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Result<ForgotPasswordCommandResponse>>
    {
        private readonly IUserServices _userService;

        public ForgotPasswordCommandHandler(IUserServices userServices)
        {
            _userService = userServices;
        }

        public async Task<Result<ForgotPasswordCommandResponse>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Always returns the same generic response, whether or not the email
                // exists in the system, so callers can't use this endpoint to discover
                // which emails are registered.
                await _userService.GenerateResetToken(request.Email);

                return Result<ForgotPasswordCommandResponse>.Success(
                    new ForgotPasswordCommandResponse
                    {
                        Message = "If an account with that email exists, a password reset link has been sent."
                    });
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing forgot password request", ex);
            }
        }
    }
}
