using Microsoft.AspNetCore.Mvc;

namespace BCNPortal.Areas.Admin.Controllers
{
    [Area("Admin")]
    // [Authorize(Policy = "BcNode")]
    public class BackendController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {

            return View(nameof(Index));
        }
    }
}
