using Microsoft.AspNetCore.Mvc;
using ydai5.Domain;

namespace ydai5.Controllers
{
    [ApiController]
    [Route("api/items")]
    public class ItemsController : ControllerBase
    {
        private readonly ABCPOS _ABCHardware;

        public ItemsController(ABCPOS ABCHardware)
        {
            _ABCHardware = ABCHardware;
        }

        [HttpGet("GetUnitPrice")]
        public IActionResult ViewUnitPriceByItemCode(string itemCode)
        {
            var item = _ABCHardware.FindItem(itemCode);
            if (item == null)
            {
                return NotFound(new { Message = "Item not found." });
            }

            return Ok(new { UnitPrice = item.UnitPrice });
        }
    }
}