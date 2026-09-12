using System.Text.Json;
using Fix.Authentication.Application.Authentication.Commands.AddServiceCategories;
using Fix.Authentication.Application.Authentication.Commands.DeleteServiceCategory;
using Fix.Authentication.Application.Authentication.Commands.ToggleServiceCategoryStatus;
using Fix.Authentication.Application.Authentication.Commands.UpdateServiceCategory;
using Fix.Authentication.Application.Authentication.Queries.AllUser;
using Fix.Authentication.Application.Authentication.Queries.GetServiceCategoryById;
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
    public class CategoriesController : BaseController
    {
        private readonly IMediator _mediator;
        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region AddCategory
        [HttpPost("AddServiceCategory")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> AddServiceCategory([FromBody] AddServiceCategoriesRequest request)
        {
            var command = request.ToCommand();
            var addCategoryResult = await _mediator.Send(command);

            return addCategoryResult switch
            {
                { IsSuccess: true, Data: not null } => CreatedAtAction(nameof(AddServiceCategory), addCategoryResult.Data),
                { IsSuccess: true, Data: null, Message: not null } => new JsonResult(new { addCategoryResult.Message }),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(addCategoryResult.Errors),
                _ => BadRequest("Invalid Fields for Add ServiceCategory ")
            };
        }
        #endregion

        #region GetAllServiceCategory
        // activeOnly=true  -> what customers should see when browsing services
        // activeOnly omitted/false -> full list, for Admin's service management screen
        [HttpGet("all-ServiceCategory")]
        public async Task<IActionResult> AllServiceCategory([FromQuery] bool? activeOnly, CancellationToken cancellationToken)
        {
            var query = new GetAllServiceCategoriesQuery(activeOnly);
            var serviceCategoryResult = await _mediator.Send(query, cancellationToken);

            return serviceCategoryResult switch
            {
                { IsSuccess: true, Data: not null } => new JsonResult(serviceCategoryResult.Data, new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                }),
                { IsSuccess: true, Data: null, Message: not null } => new JsonResult(new { Message = serviceCategoryResult.Message }),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(serviceCategoryResult.Errors),
                _ => BadRequest("An error occurred while fetching service categories")
            };
        }
        #endregion

        #region GetServiceCategoryById
        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceCategoryById(string id, CancellationToken cancellationToken)
        {
            var query = new GetServiceCategoryByIdQuery(id);
            var result = await _mediator.Send(query, cancellationToken);

            return result switch
            {
                { IsSuccess: true, Data: not null } => Ok(result.Data),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(result.Errors),
                _ => BadRequest("Service category not found.")
            };
        }
        #endregion

        #region UpdateServiceCategory
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> UpdateServiceCategory(string id, [FromBody] UpdateServiceCategoryRequest request)
        {
            var command = request.ToCommand(id);
            var result = await _mediator.Send(command);

            return result switch
            {
                { IsSuccess: true, Data: not null } => Ok(result.Data),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(result.Errors),
                _ => BadRequest("Unable to update service category.")
            };
        }
        #endregion

        #region ToggleServiceCategoryStatus
        [HttpPatch("{id}/toggle-status")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> ToggleServiceCategoryStatus(string id)
        {
            var command = new ToggleServiceCategoryStatusCommand(id);
            var result = await _mediator.Send(command);

            return result switch
            {
                { IsSuccess: true, Data: not null } => Ok(result.Data),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(result.Errors),
                _ => BadRequest("Unable to update service category status.")
            };
        }
        #endregion

        #region DeleteServiceCategory
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteServiceCategory(string id)
        {
            var command = new DeleteServiceCategoryCommand(id);
            var result = await _mediator.Send(command);

            return result switch
            {
                { IsSuccess: true, Data: not null } => Ok(result.Data),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(result.Errors),
                _ => BadRequest("Unable to delete service category.")
            };
        }
        #endregion
    }
}
