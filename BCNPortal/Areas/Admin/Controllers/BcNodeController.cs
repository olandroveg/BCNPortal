using BCNPortal.DTO.Filter;
using BCNPortal.Models;
using BCNPortal.Services.ApiRequest;
using BCNPortal.Services.BcnUser;
using BCNPortal.Utility;
using Microsoft.AspNetCore.Identity;
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
        private readonly ITokenRequestService _tokenService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IBcnUserService _bcnUserService;
        private TokenPlusId? _tokenPlusId;

        public BcNodeController
            (IBcnUserService bcnUserService,
            ITokenRequestService tokenService,
            UserManager<IdentityUser> userManager)
            {
            _tokenService = tokenService;
            _bcnUserService = bcnUserService;
            _userManager = userManager;
            _tokenPlusId = null;
            }
       
        
        [HttpGet]
        public async Task< IActionResult> Index()
        {
            //var userId = ClaimsPrincipalExtensions.GetUserId(User);
            _tokenPlusId = await GetToken();
            return View(nameof(Index));
        }
        private async Task<TokenPlusId> GetToken()
        {
            // get username of the logged user
            var userId = Guid.Empty;
            var bcnPassword = "";
            var bcnUsername = "";
            var token = new TokenPlusId();
            var loggedUser = await _userManager.GetUserAsync(HttpContext.User);
            if (loggedUser != null)
            {
                userId = Guid.Parse(await _userManager.GetUserIdAsync(loggedUser));
                var bcnUser2 = _bcnUserService.GetBcnUserAccountByUserPortalId(userId);
                bcnPassword = bcnUser2.BcnPassword;
                bcnUsername = bcnUser2.BcnUsername;

            }
            if (bcnUsername != "" && bcnPassword != "")
                token = await _tokenService.ManageToken(bcnUsername, bcnPassword, userId);
            return token;
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


        [HttpPost]
        public IActionResult LoadBcNodes()
        {
            JsonResult result = null;

            try
            {
                var search = Request.Form["search[value]"][0];
                var draw = Request.Form["draw"][0];
                var orderDir = Request.Form["order[0][dir]"];
                var order = int.Parse(Request.Form["order[0][column]"][0]);
                var startRec = Convert.ToInt32(Request.Form["start"][0]);
                var pageSize = Convert.ToInt32(Request.Form["length"][0]);
                var filters = new BaseFilter();
                filters.Page = startRec / pageSize;
                filters.PageSize = pageSize;
                if (ClaimsPrincipalExtensions.HasRoleAdmin(User))
                    filters.IsAdmin = true;
                //else
                //    filters.Userid = ClaimsPrincipalExtensions.GetUserId(User);
                // aqui filters.Userid ponemos el userId del usuario del BCN que hubo insertado los bcNodes (admin@gmail, bcNode@gmail)

                filters.Userid = _tokenPlusId != null ? _tokenPlusId.BcnUserId : Guid.Empty;
                var data = _bcNodeAdapter.ConvertBcNodesToDTOs(_bcNodeService.GetBcNodes(filters));
                var total = data.Count();
                result = Json(new
                {
                    draw = Convert.ToInt32(draw),
                    recordsTotal = total,
                    recordsFiltered = total,
                    data
                }, new JsonSerializerOptions());
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return result;
        }
    }
}
