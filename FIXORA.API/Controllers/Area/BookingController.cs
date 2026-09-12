using Fix.Authentication.Application.Authentication.Commands.AcceptJob;
using Fix.Authentication.Application.Authentication.Commands.CancelBooking;
using Fix.Authentication.Application.Authentication.Commands.CreateBooking;
using Fix.Authentication.Application.Authentication.Commands.UpdateJobStatus;
using Fix.Authentication.Application.Authentication.Queries.GetAllBookings;
using Fix.Authentication.Application.Authentication.Queries.GetBookingById;
using Fix.Authentication.Application.Authentication.Queries.GetJobRequests;
using Fix.Authentication.Application.Authentication.Queries.GetMyBookings;
using Fix.Authentication.Application.Authentication.Queries.GetMyJobs;
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
    public class BookingController : BaseController
    {
        private readonly IMediator _mediator;

        public BookingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Create (Customer)
        [HttpPost]
        [Authorize(Policy = "CustomerOnly")]
        public async Task<IActionResult> Create([FromBody] CreateBookingRequest request)
        {
            var command = request.ToCommand(CurrentUserId);
            var result = await _mediator.Send(command);

            return result switch
            {
                { IsSuccess: true, Data: not null } => CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result.Data),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(result.Errors),
                _ => BadRequest("Unable to create booking.")
            };
        }
        #endregion

        #region My Bookings (Customer)
        [HttpGet("my-bookings")]
        [Authorize(Policy = "CustomerOnly")]
        public async Task<IActionResult> GetMyBookings([FromQuery] string? status)
        {
            var query = new GetMyBookingsQuery(CurrentUserId, status);
            var result = await _mediator.Send(query);

            return result switch
            {
                { IsSuccess: true, Data: not null } => Ok(result.Data),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(result.Errors),
                _ => BadRequest("Unable to fetch bookings.")
            };
        }
        #endregion

        #region Job Requests - unassigned pending jobs (Technician)
        [HttpGet("job-requests")]
        [Authorize(Policy = "TechnicianOnly")]
        public async Task<IActionResult> GetJobRequests()
        {
            var result = await _mediator.Send(new GetJobRequestsQuery());

            return result switch
            {
                { IsSuccess: true, Data: not null } => Ok(result.Data),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(result.Errors),
                _ => BadRequest("Unable to fetch job requests.")
            };
        }
        #endregion

        #region My Jobs - accepted jobs (Technician)
        [HttpGet("my-jobs")]
        [Authorize(Policy = "TechnicianOnly")]
        public async Task<IActionResult> GetMyJobs([FromQuery] string? status)
        {
            var query = new GetMyJobsQuery(CurrentUserId, status);
            var result = await _mediator.Send(query);

            return result switch
            {
                { IsSuccess: true, Data: not null } => Ok(result.Data),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(result.Errors),
                _ => BadRequest("Unable to fetch your jobs.")
            };
        }
        #endregion

        #region All Bookings (Admin)
        [HttpGet("all")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> GetAll([FromQuery] string? status)
        {
            var query = new GetAllBookingsQuery(status);
            var result = await _mediator.Send(query);

            return result switch
            {
                { IsSuccess: true, Data: not null } => Ok(result.Data),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(result.Errors),
                _ => BadRequest("Unable to fetch bookings.")
            };
        }
        #endregion

        #region Get By Id (Customer/Technician who owns it, or Admin)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var query = new GetBookingByIdQuery(id, CurrentUserId, CurrentUserRole);
            var result = await _mediator.Send(query);

            return result switch
            {
                { IsSuccess: true, Data: not null } => Ok(result.Data),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(result.Errors),
                _ => BadRequest("Booking not found.")
            };
        }
        #endregion

        #region Accept Job (Technician)
        [HttpPatch("{id}/accept")]
        [Authorize(Policy = "TechnicianOnly")]
        public async Task<IActionResult> AcceptJob(string id)
        {
            var command = new AcceptJobCommand(id, CurrentUserId);
            var result = await _mediator.Send(command);

            return result switch
            {
                { IsSuccess: true, Data: not null } => Ok(result.Data),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(result.Errors),
                _ => BadRequest("Unable to accept job.")
            };
        }
        #endregion

        #region Update Job Status - InProgress / Completed (Technician)
        [HttpPatch("{id}/status")]
        [Authorize(Policy = "TechnicianOnly")]
        public async Task<IActionResult> UpdateJobStatus(string id, [FromBody] UpdateJobStatusRequest request)
        {
            var command = request.ToCommand(id, CurrentUserId);
            var result = await _mediator.Send(command);

            return result switch
            {
                { IsSuccess: true, Data: not null } => Ok(result.Data),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(result.Errors),
                _ => BadRequest("Unable to update job status.")
            };
        }
        #endregion

        #region Cancel (Customer)
        [HttpPatch("{id}/cancel")]
        [Authorize(Policy = "CustomerOnly")]
        public async Task<IActionResult> Cancel(string id)
        {
            var command = new CancelBookingCommand(id, CurrentUserId);
            var result = await _mediator.Send(command);

            return result switch
            {
                { IsSuccess: true, Data: not null } => Ok(result.Data),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(result.Errors),
                _ => BadRequest("Unable to cancel booking.")
            };
        }
        #endregion
    }
}
