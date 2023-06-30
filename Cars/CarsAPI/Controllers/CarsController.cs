using Microsoft.AspNetCore.Mvc;

namespace CarsAPI.Controllers
{
    [Route("api/[controller]/[action]/")]
    public class CarsController : Controller
    {
        public IActionResult Test()
        {
            return Ok();
        }
    }
}
