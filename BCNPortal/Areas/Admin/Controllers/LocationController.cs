﻿using System;
using System.Text.Json;
using BCNPortal.Areas.Admin.Models.Location;
using BCNPortal.DTO.Filter;
using BCNPortal.Models;
using BCNPortal.Services.ApiRequest;
using BCNPortal.Services.BcnUser;
using BCNPortal.Services.Location;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BCNPortal.Areas.Admin.Controllers
{
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
        public IActionResult Add()
        {
            ViewBag.Title = NewTitle;
            return View(nameof(Add), BuildLocationViewModel(null));
        }
        private async Task < CreateEditLocationViewModel> BuildLocationViewModel(Guid? locationId, int optionTab = 1)
        {

            if (locationId == null)
                return new CreateEditLocationViewModel
                {

                };
            var tokenPlusId = await GetToken();
            var locationDto = _locationService.GetLocations(tokenPlusId, locationId ?? Guid.Empty));

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
    }
}

