using Fix.Authentication.Application.Authentication.Commands.AddUser;
using Fix.Authentication.Application.Authentication.Commands.ChangePassword;
using Fix.Authentication.Application.Authentication.Commands.UpdateProfile;
using Fix.Authentication.Application.Authentication.Queries.GetMyProfile;
using FIXORA.API.BaseControllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace FIXORA.API.Controllers.Area
{

    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowReactApp")]
    public class UserController : BaseController
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }




        #region AddUser
        [HttpPost("AddUser")]

        public async Task<IActionResult> AddUser([FromBody] AddUserRequest request)
        {
            //Mapping command and request

            var command = request.ToCommand();
            var addLedgerResult = await _mediator.Send(command);
            #region Switch Statement
            return addLedgerResult switch
            {
                { IsSuccess: true, Data: not null } => CreatedAtAction(nameof(AddUser), addLedgerResult.Data),
                { IsSuccess: true, Data: null, Message: not null } => new JsonResult(new { addLedgerResult.Message }),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(addLedgerResult.Errors),
                _ => BadRequest("Invalid Fields for Add Ledger ")
            };

            #endregion
        }

        #endregion

        #region My Profile (any authenticated user)
        [HttpGet("my-profile")]
        [Authorize]
        public async Task<IActionResult> GetMyProfile()
        {
            var query = new GetMyProfileQuery(CurrentUserId);
            var result = await _mediator.Send(query);

            return result switch
            {
                { IsSuccess: true, Data: not null } => Ok(result.Data),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(result.Errors),
                _ => BadRequest("Unable to fetch profile.")
            };
        }
        #endregion

        #region Update My Profile (any authenticated user)
        [HttpPut("my-profile")]
        [Authorize]
        public async Task<IActionResult> UpdateMyProfile([FromBody] UpdateProfileRequest request)
        {
            var command = request.ToCommand(CurrentUserId);
            var result = await _mediator.Send(command);

            return result switch
            {
                { IsSuccess: true, Data: not null } => Ok(result.Data),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(result.Errors),
                _ => BadRequest("Unable to update profile.")
            };
        }
        #endregion

        #region Change Password (any authenticated user, while logged in)
        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var command = request.ToCommand(CurrentUserId);
            var result = await _mediator.Send(command);

            return result switch
            {
                { IsSuccess: true, Data: not null } => Ok(result.Data),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(result.Errors),
                _ => BadRequest("Unable to change password.")
            };
        }
        #endregion
    }
}
