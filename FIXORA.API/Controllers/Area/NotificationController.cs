using Fix.Authentication.Application.Authentication.Commands.MarkAllNotificationsAsRead;
using Fix.Authentication.Application.Authentication.Commands.MarkNotificationAsRead;
using Fix.Authentication.Application.Authentication.Queries.GetMyNotifications;
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
    public class NotificationController : BaseController
    {
        private readonly IMediator _mediator;

        public NotificationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region My Notifications
        [HttpGet]
        public async Task<IActionResult> GetMine([FromQuery] bool? unreadOnly)
        {
            var query = new GetMyNotificationsQuery(CurrentUserId, unreadOnly);
            var result = await _mediator.Send(query);

            return result switch
            {
                { IsSuccess: true, Data: not null } => Ok(result.Data),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(result.Errors),
                _ => BadRequest("Unable to fetch notifications.")
            };
        }
        #endregion

        #region Mark As Read
        [HttpPatch("{id}/read")]
        public async Task<IActionResult> MarkAsRead(string id)
        {
            var command = new MarkNotificationAsReadCommand(id, CurrentUserId);
            var result = await _mediator.Send(command);

            return result switch
            {
                { IsSuccess: true, Data: not null } => Ok(result.Data),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(result.Errors),
                _ => BadRequest("Unable to mark notification as read.")
            };
        }
        #endregion

        #region Mark All As Read
        [HttpPatch("read-all")]
        public async Task<IActionResult> MarkAllAsRead()
        {
            var command = new MarkAllNotificationsAsReadCommand(CurrentUserId);
            var result = await _mediator.Send(command);

            return result switch
            {
                { IsSuccess: true, Data: not null } => Ok(new { Message = result.Data }),
                { IsSuccess: false, Errors: not null } => HandleFailureResult(result.Errors),
                _ => BadRequest("Unable to mark all notifications as read.")
            };
        }
        #endregion
    }
}
