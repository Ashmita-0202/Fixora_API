using Fix.Authentication.Application.Authentication.Queries.GetAllCustomers;
using Fix.Authentication.Application.Authentication.Queries.GetDashboardStats;
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
    [Authorize(Policy = "AdminOnly")]
    public class AdminController : BaseController
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Dashboard Stats
        [HttpGet("dashboard-stats")]
        public async Task<IActionResult> GetDashboardStats()
        {
            var result = await _mediator.Send(new GetDashboardStatsQuery());

            return result switch
            {
                { IsSuccess: true, Data: not null } => Ok(result.Data),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(result.Errors),
                _ => BadRequest("Unable to fetch dashboard stats.")
            };
        }
        #endregion

        #region All Customers
        [HttpGet("customers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var result = await _mediator.Send(new GetAllCustomersQuery());

            return result switch
            {
                { IsSuccess: true, Data: not null } => Ok(result.Data),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(result.Errors),
                _ => BadRequest("Unable to fetch customers.")
            };
        }
        #endregion
    }
}
