using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreNetwork.Areas.Admin.Controllers
{
    [Area("Admin")]
   // [Authorize(Policy = "Administrator")]
    public class BackendController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            
            return View();
        }
    }
}
