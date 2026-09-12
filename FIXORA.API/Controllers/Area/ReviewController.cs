using Fix.Authentication.Application.Authentication.Commands.CreateReview;
using Fix.Authentication.Application.Authentication.Queries.GetTechnicianReviews;
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
    public class ReviewController : BaseController
    {
        private readonly IMediator _mediator;

        public ReviewController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Create (Customer, on a completed booking)
        [HttpPost]
        [Authorize(Policy = "CustomerOnly")]
        public async Task<IActionResult> Create([FromBody] CreateReviewRequest request)
        {
            var command = request.ToCommand(CurrentUserId);
            var result = await _mediator.Send(command);

            return result switch
            {
                { IsSuccess: true, Data: not null } => Ok(result.Data),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(result.Errors),
                _ => BadRequest("Unable to submit review.")
            };
        }
        #endregion

        #region Get For Technician (public - shown on technician profile/listing)
        [HttpGet("technician/{technicianId}")]
        public async Task<IActionResult> GetForTechnician(string technicianId)
        {
            var query = new GetTechnicianReviewsQuery(technicianId);
            var result = await _mediator.Send(query);

            return result switch
            {
                { IsSuccess: true, Data: not null } => Ok(result.Data),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(result.Errors),
                _ => BadRequest("Unable to fetch reviews.")
            };
        }
        #endregion
    }
}
