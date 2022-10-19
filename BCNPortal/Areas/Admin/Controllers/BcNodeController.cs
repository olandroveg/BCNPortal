using BCNPortal.DTO.Filter;
using BCNPortal.Utility;
using Microsoft.AspNetCore.Mvc;

namespace BCNPortal.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BcNodeController : Controller
    {
        private const string EditTitle = "Edit bcNode";
        private const string EditTitleContent = "Edit Content";
        private const string NewTitle = "New bcNode";
        private const string NewTitleContent = "New Content";

        [HttpGet]
        public IActionResult Index()
        {
            return View(nameof(Index));
        }

        //[HttpPost]
        //public IActionResult LoadBcNodes()
        //{
        //    JsonResult result = null;

        //    try
        //    {
        //        var search = Request.Form["search[value]"][0];
        //        var draw = Request.Form["draw"][0];
        //        var orderDir = Request.Form["order[0][dir]"];
        //        var order = int.Parse(Request.Form["order[0][column]"][0]);
        //        var startRec = Convert.ToInt32(Request.Form["start"][0]);
        //        var pageSize = Convert.ToInt32(Request.Form["length"][0]);
        //        var filters = new BaseFilter();
        //        filters.Page = startRec / pageSize;
        //        filters.PageSize = pageSize;
        //        if (ClaimsPrincipalExtensions.HasRoleAdmin(User))
        //            filters.IsAdmin = true;
        //        else
        //            filters.Userid = ClaimsPrincipalExtensions.GetUserId(User);
        //        var data = _bcNodeAdapter.ConvertBcNodesToDTOs(_bcNodeService.GetBcNodes(filters));
        //        var total = data.Count();
        //        result = Json(new
        //        {
        //            draw = Convert.ToInt32(draw),
        //            recordsTotal = total,
        //            recordsFiltered = total,
        //            data
        //        }, new JsonSerializerOptions());
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Write(ex);
        //    }
        //    return result;
        //}
    }
}
