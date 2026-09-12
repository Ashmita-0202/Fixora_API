using Fix.Authentication.Application.Authentication.Commands.ForgotPassword;
using Fix.Authentication.Application.Authentication.Commands.Login;
using Fix.Authentication.Application.Authentication.Commands.ResetPassword;
using FIXORA.API.BaseControllers;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace FIXORA.API.Controllers.Area
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowReactApp")]
    public class AuthController : BaseController
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var command = request.ToCommand();
            var result = await _mediator.Send(command);

            return result switch
            {
                { IsSuccess: true, Data: not null } => Ok(result.Data),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(result.Errors),
                _ => BadRequest("Invalid login request.")
            };
        }
        #endregion

        #region ForgotPassword
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            var command = request.ToCommand();
            var result = await _mediator.Send(command);

            return result switch
            {
                { IsSuccess: true, Data: not null } => Ok(result.Data),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(result.Errors),
                _ => BadRequest("Invalid forgot password request.")
            };
        }
        #endregion

        #region ResetPassword
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var command = request.ToCommand();
            var result = await _mediator.Send(command);

            return result switch
            {
                { IsSuccess: true, Data: not null } => Ok(result.Data),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(result.Errors),
                _ => BadRequest("Invalid reset password request.")
            };
        }
        #endregion
    }
}
