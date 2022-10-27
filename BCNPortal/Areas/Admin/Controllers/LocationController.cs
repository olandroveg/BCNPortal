using System;
using System.Text.Json;
using BCNPortal.Areas.Admin.Models.Location;
using BCNPortal.DTO.Filter;
using BCNPortal.DTO.Location;
using BCNPortal.Models;
using BCNPortal.Services.ApiRequest;
using BCNPortal.Services.BcnUser;
using BCNPortal.Services.Location;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BCNPortal.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LocationController: Controller
    {
        private readonly ILocationService _locationService;
        private const string EditTitle = "Edit location";
        private const string NewTitle = "New location";
        private readonly ITokenRequestService _tokenService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IBcnUserService _bcnUserService;

        public LocationController(ILocationService locationService,
            ITokenRequestService tokenService,
            UserManager<IdentityUser> userManager,
            IBcnUserService bcnUserService
            )
        {
            _locationService = locationService;
            _userManager = userManager;
            _tokenService = tokenService;
            _bcnUserService = bcnUserService;
        }
        [HttpGet]
        public IActionResult Index() => View(nameof(Index));
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
        public async Task<IActionResult> LoadLocation()
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
                //var data = _locationAdapter.ConvertLocationsToDTOs(_locationService.GetLocations(filters));
                var data = await _locationService.GetLocations(tokenPlusId, filters);
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
            return View(nameof(Add),await BuildLocationViewModel(null));
        }
        private async Task < CreateEditLocationViewModel> BuildLocationViewModel(Guid? locationId, int optionTab = 1)
        {

            if (locationId == null)
                return new CreateEditLocationViewModel
                {

                };
            var tokenPlusId = await GetToken();
            var locationDto = new LocationDto();
            try
            {
                locationDto = await _locationService.GetSingleLocation(tokenPlusId, locationId ?? Guid.Empty);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }
             

            var model = new CreateEditLocationViewModel
            {
                locationId = locationDto.Id.ToString(),
                Latitude = locationDto.Latitude,
                Longitude = locationDto.Longitude,
                Location = locationDto.Name,
                Description = locationDto.State,
                Country = locationDto.Country

            };
            return model;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CreateEditLocationViewModel model)
        {
            var tokenPlusId = await GetToken();
            var locationId = Guid.Empty;
            try
            {
                locationId = await _locationService.SendLocationDto(tokenPlusId, model.ModelToDto());
            }
            catch(Exception e)
            {
                Console.Error.WriteLine(e);
            }
            if (locationId == Guid.Empty)
                TempData["SuccLocationCreated"] = false;
            TempData["SuccLocationCreated"] = true;
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRange(IEnumerable<Guid> locations)
        {
            var tokenPlusId = await GetToken();
            try
            {
                var response = await _locationService.DeleteRange(tokenPlusId, locations);
                return Json(new { action = "success", title = "Info", message = "Selected locations has been deleted" });
            }
            catch (ArgumentException)
            {
                return Json(new { action = "error", title = "Not completed", message = "Some locations could not be deleted, not found " });
            }
        }
        [HttpGet]
        public async Task< IActionResult> Edit(Guid locationId, int optionTab = 1)
        {
            if (locationId.Equals(Guid.Empty))
                return RedirectToAction(nameof(Index));
            ViewBag.Title = EditTitle;
            return View(nameof(Edit), await BuildLocationViewModel(locationId, optionTab));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateEditLocationViewModel model)
        {
            var tokenPlusId = await GetToken();
            var locationId = Guid.Empty;
            try
            {
                locationId = await _locationService.SendLocationDto(tokenPlusId, model.ModelToDto());
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }
            if (locationId == Guid.Empty)
                TempData["SucclocationUpdated"] = false;
            TempData["SucclocationUpdated"] = true;
            return RedirectToAction(nameof(Index));
        }

    }
}

