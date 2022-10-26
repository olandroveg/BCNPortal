using Microsoft.AspNetCore.Mvc;

namespace BCNPortal.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContentController : Controller
    {
        private readonly IContentService _contentService;

        public IActionResult Index()
        {
            return View();
        }
    }
}
