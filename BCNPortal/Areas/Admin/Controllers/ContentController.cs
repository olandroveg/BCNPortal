using CoreNetwork.Adapters.Content;
using CoreNetwork.Adapters.Services;
using CoreNetwork.Areas.Admin.Models.Content;
using CoreNetwork.DTO.Filter;
using CoreNetwork.Services.Content;
using CoreNetwork.Services.ContentService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoreNetwork.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "BcNode")]
    public class ContentController : Controller
    {
        private readonly IContentService _contentService;
        private readonly IContentAdapter _contentAdapter;
        private readonly IServicesAdapter _servicesAdapter;
        private readonly IServicesService _servicesService;
        private const string EditTitle = "Edit content";
        private const string NewTitle = "New content";

        public ContentController(IContentService contentService, IContentAdapter contentAdapter,
                                 IServicesAdapter servicesAdapter, IServicesService servicesService)
        {
            _contentService = contentService;
            _contentAdapter = contentAdapter;
            _servicesAdapter = servicesAdapter;
            _servicesService = servicesService;
        }
        [HttpGet]
        public IActionResult Index() => View(nameof(Index));
        [HttpPost]
        public IActionResult LoadContents()
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
                var data = _contentAdapter.ConvertContentsToTableDtos(_contentService.GetAllContents());
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
            return View(nameof(Add), BuildContentViewModel(null));
        }
        public CreateEditContentViewModel BuildContentViewModel (Guid? Id, int optionTab = 1)
        {
            var services = _servicesAdapter.ConvertServiceToDto(_servicesService.GetServices());
            if (Id == Guid.Empty || Id == null)
            {
                return new CreateEditContentViewModel
                {
                    Services = services
                };
            }
            var contentDto = _contentAdapter.ConvertContentToDto(_contentService.GetContentById(Id));
            return _contentAdapter.ConvertDtoToViewModel(contentDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CreateEditContentViewModel model)
        {
            var contentId = await _contentService.AddOrUpdate(_contentAdapter.ConvertDtoToContent(model.ConvertToDto()));
            TempData["SuccContentCreated"] = true;
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRange(IEnumerable<Guid> contentIds)
        {
            try
            {
                await _contentService.DeleteRange(contentIds);
                return Json(new { action = "success", title = "Info", message = "Selected contents has been deleted" });
            }
            catch (ArgumentException)
            {
                return Json(new { action = "error", title = "Not completed", message = "Some contents could not be deleted, not found " });
            }
        }
        [HttpGet]
        public IActionResult Edit(Guid contentId, int optionTab = 1)
        {
            if (contentId.Equals(Guid.Empty))
                return RedirectToAction(nameof(Index));
            ViewBag.Title = EditTitle;
            return View(nameof(Edit), BuildContentViewModel(contentId, optionTab));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateEditContentViewModel model)
        {
            var contentId = await _contentService.AddOrUpdate(_contentAdapter.ConvertDtoToContent(model.ConvertToDto()));
            TempData["SuccContentUpdated"] = true;
            return RedirectToAction(nameof(Index));
        }
    }
}
