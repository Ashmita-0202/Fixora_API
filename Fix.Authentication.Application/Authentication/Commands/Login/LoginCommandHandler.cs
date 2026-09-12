using Fix.Authentication.Application.ServiceInterface;
using Fix.Shared.Domain.Abstractions;
using MediatR;

namespace Fix.Authentication.Application.Authentication.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginCommandResponse>>
    {
        private readonly IUserServices _userService;

        public LoginCommandHandler(IUserServices userServices)
        {
            _userService = userServices;
        }

        public async Task<Result<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var loginResult = await _userService.Login(request);

                if (loginResult.Errors.Any())
                {
                    var errors = string.Join(", ", loginResult.Errors);
                    return Result<LoginCommandResponse>.Failure(errors);
                }

                if (loginResult is null || !loginResult.IsSuccess)
                {
                    return Result<LoginCommandResponse>.Failure("Unable to login.");
                }

                return loginResult;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while logging in", ex);
            }
        }
    }
}
