using Fix.Authentication.Application.Authentication.Commands.ApproveTechnician;
using Fix.Authentication.Application.Authentication.Commands.RejectTechnician;
using Fix.Authentication.Application.Authentication.Commands.ToggleTechnicianActiveStatus;
using Fix.Authentication.Application.Authentication.Commands.UpdateTechnicianProfile;
using Fix.Authentication.Application.Authentication.Queries.GetAllTechnicians;
using Fix.Authentication.Application.Authentication.Queries.GetMyEarnings;
using Fix.Authentication.Application.Authentication.Queries.GetMyTechnicianProfile;
using Fix.Authentication.Application.Authentication.Queries.GetPendingTechnicianApplications;
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
    [Authorize]
    public class TechnicianController : BaseController
    {
        private readonly IMediator _mediator;

        public TechnicianController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region My Profile (Technician, self)
        [HttpGet("my-profile")]
        [Authorize(Policy = "TechnicianOnly")]
        public async Task<IActionResult> GetMyProfile()
        {
            var query = new GetMyTechnicianProfileQuery(CurrentUserId);
            var result = await _mediator.Send(query);

            return result switch
            {
                { IsSuccess: true, Data: not null } => Ok(result.Data),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(result.Errors),
                _ => BadRequest("Technician profile not found.")
            };
        }
        #endregion

        #region Pending Applications (Admin)
        [HttpGet("pending-applications")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> GetPendingApplications()
        {
            var result = await _mediator.Send(new GetPendingTechnicianApplicationsQuery());

            return result switch
            {
                { IsSuccess: true, Data: not null } => Ok(result.Data),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(result.Errors),
                _ => BadRequest("Unable to fetch pending applications.")
            };
        }
        #endregion

        #region All Approved Technicians (Admin)
        [HttpGet]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> GetAll([FromQuery] bool? activeOnly)
        {
            var query = new GetAllTechniciansQuery(activeOnly);
            var result = await _mediator.Send(query);

            return result switch
            {
                { IsSuccess: true, Data: not null } => Ok(result.Data),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(result.Errors),
                _ => BadRequest("Unable to fetch technicians.")
            };
        }
        #endregion

        #region Approve (Admin)
        [HttpPatch("{profileId}/approve")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Approve(string profileId)
        {
            var command = new ApproveTechnicianCommand(profileId);
            var result = await _mediator.Send(command);

            return result switch
            {
                { IsSuccess: true, Data: not null } => Ok(result.Data),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(result.Errors),
                _ => BadRequest("Unable to approve technician.")
            };
        }
        #endregion

        #region Reject (Admin)
        [HttpPatch("{profileId}/reject")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Reject(string profileId, [FromBody] RejectTechnicianRequest request)
        {
            var command = request.ToCommand(profileId);
            var result = await _mediator.Send(command);

            return result switch
            {
                { IsSuccess: true, Data: not null } => Ok(result.Data),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(result.Errors),
                _ => BadRequest("Unable to reject technician.")
            };
        }
        #endregion

        #region Toggle Active Status (Admin)
        [HttpPatch("{profileId}/toggle-active")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> ToggleActiveStatus(string profileId)
        {
            var command = new ToggleTechnicianActiveStatusCommand(profileId);
            var result = await _mediator.Send(command);

            return result switch
            {
                { IsSuccess: true, Data: not null } => Ok(result.Data),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(result.Errors),
                _ => BadRequest("Unable to update technician status.")
            };
        }
        #endregion

        #region Update My Technician Profile (Technician, self)
        [HttpPut("my-profile")]
        [Authorize(Policy = "TechnicianOnly")]
        public async Task<IActionResult> UpdateMyProfile([FromBody] UpdateTechnicianProfileRequest request)
        {
            var command = request.ToCommand(CurrentUserId);
            var result = await _mediator.Send(command);

            return result switch
            {
                { IsSuccess: true, Data: not null } => Ok(result.Data),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(result.Errors),
                _ => BadRequest("Unable to update your profile.")
            };
        }
        #endregion
        #region My Earnings (Technician, self)
        [HttpGet("my-earnings")]
        [Authorize(Policy = "TechnicianOnly")]
        public async Task<IActionResult> GetMyEarnings()
        {
            var query = new GetMyEarningsQuery(CurrentUserId);
            var result = await _mediator.Send(query);

            return result switch
            {
                { IsSuccess: true, Data: not null } => Ok(result.Data),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(result.Errors),
                _ => BadRequest("Unable to fetch earnings.")
            };
        }
        #endregion
    }
}
