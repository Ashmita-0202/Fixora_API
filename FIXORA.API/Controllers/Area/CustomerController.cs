using FIXORA.API.Data;
using Microsoft.AspNetCore.Mvc;

namespace FIXORA.API.Controllers.Area
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {

        private readonly ApplicationDbContext DbContext;
        public CustomerController(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;

        }
        //#region Customer
        [HttpGet("allCustomer")]
        public IActionResult GetAllCustomers()
        {
            var allCustomer = DbContext.Customers.ToList();
            return Ok(allCustomer);
        }
    }
    //#endregion
}
