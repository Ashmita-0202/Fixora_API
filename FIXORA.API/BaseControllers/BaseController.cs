using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FIXORA.API.BaseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        //private readonly UserManager<ApplicationUser> _userManager;
        //private readonly RoleManager<IdentityRole> _roleManager;


        //public BaseController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        //{
        //    _userManager = userManager;
        //    _roleManager = roleManager;

        //}

        // Reads the calling user's id/role from their JWT (set in TokenService.GenerateToken).
        // Only meaningful on [Authorize]-protected endpoints.
        protected string CurrentUserId => User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        protected string CurrentUserRole => User.FindFirstValue(ClaimTypes.Role) ?? string.Empty;

        protected IActionResult HandleFailureResult(IEnumerable<string> errors)
        {
            //Check the error message and return appropriate status code

            if (errors.Any(errors => errors.Contains("Unauthorized")))
            {
                return Unauthorized(errors);
            }
            else if (errors.Any(errors => errors.Contains("NotFound", StringComparison.OrdinalIgnoreCase)))
            {
                return NotFound(errors);
            }
            else if (errors.Any(errors => errors.Contains("InsufficientFunds", StringComparison.OrdinalIgnoreCase)))
            {
                return StatusCode(402, errors);
            }
            else if (errors.Any(errors => errors.Contains("ForbiddenAccess", StringComparison.OrdinalIgnoreCase)))
            {
                return Forbid(string.Join(", ", errors));
            }

            else if (errors.Any(errors => errors.Contains("Conflict", StringComparison.OrdinalIgnoreCase)))
            {
                return Conflict(errors);
            }
            else if (errors.Any(errors => errors.Contains("NoContent", StringComparison.OrdinalIgnoreCase)))
            {
                return StatusCode(204, new { Message = "No content available.", Errors = errors });
            }
            else
            {
                return BadRequest(errors);
            }
        }
    }
}
