using Fix.Authentication.Application.ServiceInterface;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result<ResetPasswordCommandResponse>>
    {
        private readonly IUserServices _userService;

        public ResetPasswordCommandHandler(IUserServices userServices)
        {
            _userService = userServices;
        }

        public async Task<Result<ResetPasswordCommandResponse>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userService.ResetPassword(request.Email, request.Token, request.NewPassword);

                if (result.Errors.Any())
                {
                    var errors = string.Join(", ", result.Errors);
                    return Result<ResetPasswordCommandResponse>.Failure(errors);
                }

                if (result is null || !result.IsSuccess)
                {
                    return Result<ResetPasswordCommandResponse>.Failure("Unable to reset password.");
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while resetting password", ex);
            }
        }
    }
}
