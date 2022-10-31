using System.Text.Json;
using BCNPortal.Areas.Admin.Models.BcNode;
using BCNPortal.Areas.Admin.Models.BcNodeContent;
using BCNPortal.Dto.BcNodeContent;
using BCNPortal.DTO;
using BCNPortal.DTO.BcNode;
using BCNPortal.DTO.Filter;
using BCNPortal.Models;
using BCNPortal.Services.ApiRequest;
using BCNPortal.Services.BcNodeContent;
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
        private readonly IBcNodeContentService _bcNodeContentService;
        



        public BcNodeController
            (IBcnUserService bcnUserService,
            ITokenRequestService tokenService,
            UserManager<IdentityUser> userManager,
            IBcNodeService bcNodeService,
            ILocationService locationService,
            IBcNodeContentService bcNodeContentService)

            {
                _tokenService = tokenService;
                _bcnUserService = bcnUserService;
                _userManager = userManager;
                 _bcNodeService = bcNodeService;
                _locationService = locationService;
                _bcNodeContentService = bcNodeContentService;
            }
       
        
        [HttpGet]
        public  IActionResult Index()
        {
            
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
        


        [HttpPost]
        public async Task <IActionResult> LoadBcNodes()
        {
            JsonResult result = null;

            
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
                if (tokenPlusId == null || tokenPlusId.Token == "")
                    throw new ArgumentNullException();
                var data = new List<BcNodeDto>();
                try 
                {
                    data = await _bcNodeService.GetBcNodes(tokenPlusId, filters);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex);
                }
                
                var total = data.Count();
                result = Json(new
                {
                    draw = Convert.ToInt32(draw),
                    recordsTotal = total,
                    recordsFiltered = total,
                    data
                }, new JsonSerializerOptions());
                return result;
        }
        [HttpGet]
        public async Task< IActionResult> Add()
        {
            ViewBag.Title = NewTitle;
            return View(nameof(Add), await BuildBcNodeViewModel(null));
        }
        
        
        private async Task< CreateEditBcNodeViewModel> BuildBcNodeViewModel(Guid? bcNodeId, int optionTab = 1)
        {
            var tokenPlusId = await GetToken();
            var locations = await _locationService.GetAllLocations(tokenPlusId);
            
            var bcNodeEnum = await _bcNodeService.GetAllBcNodes(tokenPlusId).ContinueWith(x => x.Result.Select(x => new BaseDTO
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
            var bcNodeList = bcNodeEnum.ToList();
            bcNodeList.Insert(0, new BaseDTO
            {
                Id = Guid.Empty,
                Name = "Do not apply"
            });
            if (bcNodeId == null)
                return new CreateEditBcNodeViewModel
                {
                    Locations = locations,
                    bcNodeList = bcNodeList,
                    Latitude = " ",
                    Longitude = " "
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
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CreateEditBcNodeViewModel model, bool continueEditing)
        {
            var tokenPlusId = await GetToken();
            model.UserId = tokenPlusId.BcnUserId;
            var locationId = Guid.Empty;
            try
            {
                locationId = await _bcNodeService.SendContentDto(tokenPlusId,model.ModelToDto());
            }
            catch (Exception ex)
            {
                TempData["SuccbcNodeCreated"] = false;
                Console.Error.WriteLine(ex);
            }
            TempData["SuccbcNodeCreated"] = true;
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task< IActionResult> Edit(Guid bcnodeId, int optionTab = 1)
        {
            if (bcnodeId.Equals(Guid.Empty))
                return RedirectToAction(nameof(Index));
            ViewBag.Title = EditTitle;
            return View(nameof(Edit), await BuildBcNodeViewModel(bcnodeId, optionTab));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateEditBcNodeViewModel model)
        {
            var tokenPlusId = await GetToken();
            var bcnodeId = Guid.Empty;
            try
            {
                bcnodeId = await _bcNodeService.SendContentDto(tokenPlusId, model.ModelToDto());
                TempData["SuccbcNodeUpdated"] = true;
            }
            catch (Exception ex)
            {
                TempData["SuccbcNodeUpdated"] = false;
                Console.Error.WriteLine(ex);
                //return BadRequest(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task< IActionResult> LoadBcNodeContents(Guid bcNodeId)
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
                filters.RefenceId = bcNodeId;
                var data = await _bcNodeContentService.GetBcNodeContentDto(tokenPlusId, filters);
                var total = data.Count();
                result = Json(new
                {
                    draw = Convert.ToInt32(draw),
                    recordsTotal = total,
                    recordsFiltered = total,
                    data
                }, new JsonSerializerOptions());
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
            return result;
        }
        [HttpGet]
        public async Task <IActionResult> AddBcNodeContent(string bcNodeId)
        {
            ViewBag.Title = NewTitleContent;
            return View(nameof(AddBcNodeContent),await BuildBcNodeAddContentViewModel(bcNodeId));
        }

        private async Task< AddBcNodeContentViewModel> BuildBcNodeAddContentViewModel(string bcNodeId)
        {
            var tokenPlusId = await GetToken();
            return new AddBcNodeContentViewModel
            {
                Contents = await _bcNodeContentService.GetAllContents(tokenPlusId,bcNodeId),
                
                BcNodeId = bcNodeId
            };

        }
        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBcNodeContent(AddBcNodeContentViewModel model, bool continueEditing)
        {
            var tokenPlusId = await GetToken();
            var bcnodeId = Guid.Parse(model.BcNodeId);
            try
            {
                var bcNodeContentId = await _bcNodeContentService.SendBcNodeContentDto(tokenPlusId, model.ConvertToDto());
                if (bcNodeContentId== Guid.Empty)
                {
                    TempData["SuccContentCreated"] = false;
                    throw new Exception("BadRequest");
                }
                TempData["SuccContentCreated"] = true;

            }
            catch (Exception ex)
            {
                TempData["SuccContentCreated"] = false;
                Console.Error.WriteLine(ex);
                
            }
            return View(nameof(Edit),await BuildBcNodeViewModel(bcnodeId));
        }

        [HttpGet]
        public async Task <IActionResult> EditBcNodeContent(Guid bcnodeContentId, int optionTab = 1)
        {
            if (bcnodeContentId.Equals(Guid.Empty))
                return RedirectToAction(nameof(Index));
            ViewBag.Title = EditTitleContent;
            return View(nameof(EditBcNodeContent),await BuildBcNodeContentViewModel(bcnodeContentId, optionTab));
        }

        private async Task< BcNodeContentViewModel> BuildBcNodeContentViewModel(Guid bcNodeContentId, int optionTab = 1)
        {
            var tokenPlusId = await GetToken();
            if (bcNodeContentId == Guid.Empty)
                return new BcNodeContentViewModel
                {

                };
            var bcNodeContentDto = new BcNodeContentDto();
            try
            {
                bcNodeContentDto = await _bcNodeContentService.GetBcNodeContentDto(tokenPlusId, bcNodeContentId);
                return new BcNodeContentViewModel
                {
                    Id = bcNodeContentDto.Id.ToString(),
                    BcNodeId = bcNodeContentDto.BcNodeId.ToString(),
                    ContentId = bcNodeContentDto.ContentId.ToString(),
                    Service = bcNodeContentDto.Service,
                    Bitrate = bcNodeContentDto.Bitrate.ToString(),
                    SourceLocation = bcNodeContentDto.SourceLocation,
                    Size = bcNodeContentDto.Size.ToString(),
                    Name = bcNodeContentDto.Name

                };
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return new BcNodeContentViewModel
                {

                };
            }
            
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBcNodeContent(BcNodeContentViewModel model)
        {
            var tokenPlusId = await GetToken();
            var bcnodeContentId = Guid.Empty;
            try
            {
                bcnodeContentId = await _bcNodeContentService.SendBcNodeContentDto(tokenPlusId, model.ConvertToDto());
                if (bcnodeContentId == Guid.Empty)
                    throw new Exception("empty Id");
                TempData["SuccContentUpdated"] = true;
            }
            catch(Exception ex)
            {
                TempData["SuccContentUpdated"] = false;
                Console.Error.Write(ex.Message);
            }
            var bcnodeId = Guid.Parse(model.BcNodeId);
            return View(nameof(Edit), await BuildBcNodeViewModel(bcnodeId));
        }
    }
}
