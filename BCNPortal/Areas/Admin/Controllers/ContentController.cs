using BCNPortal.Areas.Admin.Models.Content;
using BCNPortal.Dto.Service;
using BCNPortal.DTO.Filter;
using BCNPortal.Models;
using BCNPortal.Services.ApiRequest;
using BCNPortal.Services.BcnUser;
using BCNPortal.Services.Content;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BCNPortal.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContentController : Controller
    {
        private const string EditTitle = "Edit content";
        private const string NewTitle = "New content";

        private readonly IContentService _contentService;
        private readonly ITokenRequestService _tokenService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IBcnUserService _bcnUserService;


        public ContentController(IBcnUserService bcnUserService,
            ITokenRequestService tokenService,
            UserManager<IdentityUser> userManager, IContentService contentService)
        {
            _contentService = contentService;
            _tokenService = tokenService;
            _userManager = userManager;
            _bcnUserService = bcnUserService;
        }

        public IActionResult Index()
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
        public async Task < IActionResult> LoadContents()
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
                filters.Userid = tokenPlusId.BcnUserId;
                var data = await _contentService.GetAllContents(tokenPlusId);
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

        public async Task <CreateEditContentViewModel> BuildContentViewModel(Guid? Id, int optionTab = 1)
        {
            var tokenPlusId = await GetToken();
            var services = await _contentService.GetAllServices(tokenPlusId);
            if (Id == Guid.Empty || Id == null)
            {
                return new CreateEditContentViewModel
                {
                    Services = (IEnumerable<ServiceDto>)services
                };
            }
            var contentDto = _contentAdapter.ConvertContentToDto(_contentService.GetContentById(Id));
            return _contentAdapter.ConvertDtoToViewModel(contentDto);
        }
        [HttpGet]
        public async Task< IActionResult> Add()
        {
            ViewBag.Title = NewTitle;
            return View(nameof(Add),await BuildContentViewModel(null));
        }

    }
}
