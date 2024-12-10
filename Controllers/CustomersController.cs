using Microsoft.AspNetCore.Mvc;
using ydai5.Domain;

namespace ydai5.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ABCPOS _ABCHardware;

        public CustomersController(ABCPOS ABCHardware)
        {
            _ABCHardware = ABCHardware;
        }

        [HttpGet("GetCustomer")]
        public IActionResult GetCustomerByCustomerID(int customerId)
        {
            var FoundCustomer = _ABCHardware.FindCustomer(customerId);
            if (FoundCustomer == null)
            {
                return NotFound(new { Message = "Customer not found." });
            }

            return Ok(new { FoundCustomer = FoundCustomer });
        }
    }
}