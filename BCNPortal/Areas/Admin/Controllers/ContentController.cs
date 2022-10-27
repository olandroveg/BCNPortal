using BCNPortal.Areas.Admin.Models.Content;
using BCNPortal.Dto.Content;
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
            var contentDto = new ContentDto();
            try 
            {
                contentDto = await _contentService.GetSingleContent(tokenPlusId, Id ?? Guid.Empty);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
             
            return new CreateEditContentViewModel
            {
                Id = contentDto.Id.ToString(),
                SelectedServiceId = contentDto.ServicesId,
                Services = contentDto.services,
                SourceLocation = contentDto.SourceLocation,
                Name = contentDto.Name
            };
        }
        [HttpGet]
        public async Task< IActionResult> Add()
        {
            ViewBag.Title = NewTitle;
            return View(nameof(Add),await BuildContentViewModel(null));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CreateEditContentViewModel model)
        {
            var tokenPlusId = await GetToken();
            var contentId = Guid.Empty;
            try 
            {
                contentId = await _contentService.SendContentDto(tokenPlusId, model.ConvertToDto());
            }
            catch(Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
            
            TempData["SuccContentCreated"] = true;
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRange(IEnumerable<Guid> contentIds)
        {
            var tokenPlusId = await GetToken();
            try
            {
                await _contentService.DeleteRange(tokenPlusId,contentIds);
                return Json(new { action = "success", title = "Info", message = "Selected contents has been deleted" });
            }
            catch (ArgumentException)
            {
                return Json(new { action = "error", title = "Not completed", message = "Some contents could not be deleted, not found " });
            }
        }
        [HttpGet]
        public async Task< IActionResult> Edit(Guid contentId, int optionTab = 1)
        {
            if (contentId.Equals(Guid.Empty))
                return RedirectToAction(nameof(Index));
            ViewBag.Title = EditTitle;
            return View(nameof(Edit),await BuildContentViewModel(contentId, optionTab));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateEditContentViewModel model)
        {
            var tokenPlusId = await GetToken();
            var contentId = Guid.Empty;
            try
            {
                contentId = await _contentService.SendContentDto(tokenPlusId, model.ConvertToDto());
            }
            catch (Exception ex)
            {
                TempData["SuccContentUpdated"] = false;
                Console.Error.WriteLine(ex);
            }
            if (contentId == Guid.Empty)
                TempData["SuccContentUpdated"] = false;

             
            TempData["SuccContentUpdated"] = true;
            return RedirectToAction(nameof(Index));
        }

    }
}
