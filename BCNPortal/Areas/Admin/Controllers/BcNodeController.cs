using System.Text.Json;
using BCNPortal.Areas.Admin.Models.BcNode;
using BCNPortal.DTO;
using BCNPortal.DTO.BcNode;
using BCNPortal.DTO.Filter;
using BCNPortal.Models;
using BCNPortal.Services.ApiRequest;
using BCNPortal.Services.BcNodeRqst;
using BCNPortal.Services.BcnUser;
using BCNPortal.Services.Location;
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
        private readonly IBcNodeService _bcNodeService;
        private readonly ILocationService _locationService;



        public BcNodeController
            (IBcnUserService bcnUserService,
            ITokenRequestService tokenService,
            UserManager<IdentityUser> userManager,
            IBcNodeService bcNodeService,
            ILocationService locationService)


            {
                _tokenService = tokenService;
                _bcnUserService = bcnUserService;
                _userManager = userManager;
                 _bcNodeService = bcNodeService;
                _locationService = locationService;
            }
       
        
        [HttpGet]
        public  IActionResult Index()
        {
            //var userId = ClaimsPrincipalExtensions.GetUserId(User);
            
            return View(nameof(Index));
        }
        private async Task<TokenPlusId> GetToken()
        {
            // get username of the logged user
            var bcnPassword = "";
            var bcnUsername = "";
            var userId = Guid.Empty;
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
        public async Task <IActionResult> LoadBcNodes()
        {
            JsonResult result = null;

            try
            {
                var tokenPlusId = await GetToken();
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

                filters.Userid = tokenPlusId != null ? tokenPlusId.BcnUserId : Guid.Empty;
                var data = new List<BcNodeDto>();
                if (tokenPlusId != null && tokenPlusId.Token != "")
                    data = await _bcNodeService.GetBcNodes(tokenPlusId, filters);
                if (data.Count() == 1 && data.ElementAt(0).Description == "unauthorized")
                {
                    tokenPlusId = await GetToken();
                    data = await _bcNodeService.GetBcNodes(tokenPlusId, filters);
                }
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
        [HttpGet]
        public async Task< IActionResult> Add()
        {
            ViewBag.Title = NewTitle;
            return View(nameof(Add), BuildBcNodeViewModel(null));
        }
        
        
        private async Task< CreateEditBcNodeViewModel> BuildBcNodeViewModel(Guid? bcNodeId, int optionTab = 1)
        {
            var tokenPlusId = await GetToken();
            var locations = await _locationService.GetAllLocations(tokenPlusId);
            //var bcNodeList = _bcNodeAdapter.ConvertBcNodesToDTOs(_bcNodeService.GetAllBcNodes()).Select(x => new BaseDTO
            //{
            //    Id = x.Id,
            //    Name = x.Name
            //}).ToList();
            var bcNodeList = await _bcNodeService.GetAllBcNodes(tokenPlusId).ContinueWith(x => x.Result.Select(x => new BaseDTO
            {
                Id = x.Id,
                Name = x.Name
            }));
	         
	        
            //var bcNodes = await _bcNodeService.GetAllBcNodes(tokenPlusId);
            //var bcNodeList = bcNodes.Select(x => new BaseDTO
            //{
            //    Id = x.Id,
            //    Name = x.Name
            //}).ToList();

            bcNodeList.ToList().Insert(0, new BaseDTO
            {
                Id = Guid.Empty,
                Name = "Do not apply"
            });

            if (bcNodeId == null)
                return new CreateEditBcNodeViewModel
                {
                    Locations = locations,
                    bcNodeList = bcNodeList
                };
            try
            {
                var bcnodeDto = await _bcNodeService.GetBcNode(tokenPlusId, bcNodeId ?? Guid.Empty);
                return new CreateEditBcNodeViewModel
                {
                    Name = bcnodeDto.Name,
                    Description = bcnodeDto.Description,
                    SelectedLocationId = bcnodeDto.PlaceId,
                    Locations = locations,
                    BcNodeId = bcnodeDto.Id.ToString(),
                    UserId = bcnodeDto.UserId,
                    SelectedBcNodeId = bcnodeDto.TopBcNode,
                    bcNodeList = bcNodeList,
                    Group = bcnodeDto.Group,
                    Latitude = bcnodeDto.Latitude,
                    Longitude = bcnodeDto.Longitude
                };
            }
            catch (Exception e){
                throw new ArgumentNullException(e.Message);
            }

            

        }
    }
}
