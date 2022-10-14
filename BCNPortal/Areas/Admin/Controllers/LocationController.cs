using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

using CoreNetwork.Areas.Admin.Models.Location;


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CoreNetwork.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "Administrator")]
    public class LocationController : Controller
    {
        //private readonly ILocationService _locationService;
        //private readonly ILocationAdapter _locationAdapter;
        //private const string EditTitle = "Edit location";
        //private const string NewTitle = "New location";

        //public LocationController(ILocationAdapter locationAdapter, ILocationService locationService)
        //{
        //    _locationAdapter = locationAdapter;
        //    _locationService = locationService;

        //}
        //[HttpGet]
        //public IActionResult Index() => View(nameof(Index));

        //[HttpPost]
        //public IActionResult LoadLocation()
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
        //        var data = _locationAdapter.ConvertLocationsToDTOs(_locationService.GetLocations(filters));
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
        //[HttpGet]        
        //public IActionResult Add()
        //{
        //    ViewBag.Title = NewTitle;
        //    return View(nameof(Add), BuildLocationViewModel(null));
        //}
        //private CreateEditLocationViewModel BuildLocationViewModel(Guid? locationId, int optionTab = 1)
        //{
        //    if (locationId == null)
        //        return new CreateEditLocationViewModel
        //        {

        //        };
        //    var locationDto = _locationAdapter.ConvertLocationToDTO(_locationService.GetLocation(locationId));
        //    var model = new CreateEditLocationViewModel
        //    {
        //        locationId = locationDto.Id.ToString(),
        //        Latitude = locationDto.Latitude,
        //        Longitude = locationDto.Longitude,
        //        Location = locationDto.Name,
        //        Description = locationDto.State,
        //        Country = locationDto.Country

        //    };
        //    return model;
        //}
        //[HttpPost]        
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Add(CreateEditLocationViewModel model)
        //{
        //    var locationId = await _locationService.AddOrUpdate(_locationAdapter.ConvertDtoToLocation(model.ModelToDto()));
        //    TempData["SuccLocationCreated"] = true;
        //    return RedirectToAction(nameof(Index));
        //}
        //[HttpPost]
        //public async Task< IActionResult> DeleteRange(IEnumerable<Guid> locations)
        //{
        //    try
        //    {
        //       await _locationService.DeleteRange(locations);
        //        return Json(new { action = "success", title = "Info", message = "Selected locations has been deleted" });
        //    }
        //    catch (ArgumentException)
        //    {
        //        return Json(new { action = "error", title = "Not completed", message = "Some locations could not be deleted, not found " });
        //    }
        //}
        //[HttpGet]
        //public IActionResult Edit(Guid locationId, int optionTab = 1)
        //{
        //    if (locationId.Equals(Guid.Empty))
        //        return RedirectToAction(nameof(Index));
        //    ViewBag.Title = EditTitle;
        //    return View(nameof(Edit), BuildLocationViewModel(locationId, optionTab));
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(CreateEditLocationViewModel model)
        //{
        //    var locationId = await _locationService.AddOrUpdate(_locationAdapter.ConvertDtoToLocation(model.ModelToDto()));
        //    TempData["SucclocationUpdated"] = true;
        //    return RedirectToAction(nameof(Index));
        //}

    }
}
