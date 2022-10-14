
using CoreNetwork.Areas.Admin.Models.BcNode;
using CoreNetwork.Areas.Admin.Models.BcNodeContent;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class BcNodeController : Controller
    {
        //private const string EditTitle = "Edit bcNode";
        //private const string EditTitleContent = "Edit Content";
        //private const string NewTitle = "New bcNode";
        //private const string NewTitleContent = "New Content";
        //private readonly IBcNodeService _bcNodeService;
        //private readonly IBcNodeAdapter _bcNodeAdapter;
        //private readonly ILocationService _locationService;
        //private readonly ILocationAdapter _locationAdapter;
        //private readonly IBcNodeContentAdapter _bcNodeContentAdapter;
        //private readonly IBcNodeContentService _bcNodeContentService;
        //private readonly IContentAdapter _contentAdapter;
        //private readonly IContentService _contentService;

        //public BcNodeController (IBcNodeAdapter bcNodeAdapter, IBcNodeService bcNodeService,
        //        ILocationAdapter locationAdapter, ILocationService locationService,
        //        IBcNodeContentAdapter bcNodeContentAdapter,
        //        IBcNodeContentService bcNodeContentService, IContentService contentService,
        //        IContentAdapter contentAdapter)
        //{
        //    _bcNodeAdapter = bcNodeAdapter;
        //    _bcNodeService = bcNodeService;
        //    _locationAdapter = locationAdapter;
        //    _locationService = locationService;
        //    _bcNodeContentAdapter = bcNodeContentAdapter;
        //    _bcNodeContentService = bcNodeContentService;
        //    _contentAdapter = contentAdapter;
        //    _contentService = contentService;
        //}
        //// GET: BcNodeController
        //[HttpGet]
        //public IActionResult Index()
        //{ 
        //        return View(nameof(Index)); 
        //}
        

        //[HttpPost]
        //public IActionResult LoadBcNodeContents(Guid bcNodeId)
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
        //        filters.RefenceId = bcNodeId;
        //        var data = _bcNodeContentAdapter.ConvertBcNodesContentToDTOs(_bcNodeContentService.GetBcNodeContents(filters));
        //        var total = data.Count();
        //        result = Json(new
        //        {
        //            draw = Convert.ToInt32(draw),
        //            recordsTotal = total,
        //            recordsFiltered = total,
        //            data
        //        }, new JsonSerializerOptions());
        //    }
        //    catch(Exception e)
        //    {
        //        Console.Write(e);
        //    }
        //    return result;
        //}

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

        //[HttpGet]
        //public IActionResult AddBcNodeContent(string bcNodeId)
        //{
        //    ViewBag.Title = NewTitle;
        //    return View(nameof(AddBcNodeContent), BuildBcNodeAddContentViewModel(bcNodeId));
        //}
        //[HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> AddBcNodeContent(AddBcNodeContentViewModel model, bool continueEditing)
        //{
        //    // var locationId = await _bcNodeService.AddOrUpdate(_bcNodeAdapter.ConvertDtoToBcNode(model.ModelToDto()));
        //    var bcNodeContent = await _bcNodeContentService.AddOrUpdate(_bcNodeContentAdapter.ConvertDtoToBcNodeContent(model.ConvertToDto()));
            
        //    TempData["SuccContentCreated"] = true;
        //    var bcnodeId = Guid.Parse(model.BcNodeId);
        //    return View(nameof(Edit), BuildBcNodeViewModel(bcnodeId));
        //}
        //[HttpGet]
        //public IActionResult Add()
        //{
        //    ViewBag.Title = NewTitle;
        //    return View(nameof(Add), BuildBcNodeViewModel(null));
        //}

        //private AddBcNodeContentViewModel BuildBcNodeAddContentViewModel(string bcNodeId)
        //{
        //    return new AddBcNodeContentViewModel
        //    {
        //        Contents = _contentAdapter.ConvertContentsToDtos(_contentService.GetAllContents()).Select(x => new DTO.BaseDTO
        //        {
        //            Id = x.Id,
        //            Name = x.Name
        //        }),
        //        BcNodeId = bcNodeId
        //    };
            
        //}
        //private BcNodeContentViewModel BuildBcNodeContentViewModel(Guid bcNodeContentId, int optionTab = 1)
        //{
        //    var services = _contentAdapter.ConvertContentToServiceDto(_contentService.GetAllContents());
        //    if (bcNodeContentId == Guid.Empty)
        //        return new BcNodeContentViewModel
        //        {
                    
        //        };
            
        //    var bcNodeContentDto = _bcNodeContentAdapter.ConvertBcNodeContentToDto(_bcNodeContentService.GetBcNodeContentById(bcNodeContentId));
        //    return new BcNodeContentViewModel
        //    {
        //        Id = bcNodeContentDto.Id.ToString(),
        //        BcNodeId= bcNodeContentDto.BcNodeId.ToString(),
        //        ContentId = bcNodeContentDto.ContentId.ToString(),
        //        Service = bcNodeContentDto.Service,
        //        Bitrate = bcNodeContentDto.Bitrate.ToString(),
        //        SourceLocation = bcNodeContentDto.SourceLocation,
        //        Size = bcNodeContentDto.Size.ToString(),
        //        Name = bcNodeContentDto.Name
                
        //    };
        //}
        //private CreateEditBcNodeViewModel BuildBcNodeViewModel(Guid? bcNodeId, int optionTab = 1)
        //{
        //    var locations = _locationAdapter.ConvertListDto(_locationService.GetAllLocations());
        //    var bcNodeList = _bcNodeAdapter.ConvertBcNodesToDTOs(_bcNodeService.GetAllBcNodes()).Select(x => new BaseDTO
        //    {
        //        Id = x.Id,
        //        Name = x.Name
        //    }).ToList();
        //    bcNodeList.Insert(0, new BaseDTO
        //    {
        //        Id = Guid.Empty,
        //        Name = "Do not apply"
        //    });

        //    if (bcNodeId == null)
        //        return new CreateEditBcNodeViewModel
        //        {
        //            Locations = locations,
        //            bcNodeList = bcNodeList
        //        };
        //    var bcnodeDto = _bcNodeAdapter.ConvertBcNodeToDTO(_bcNodeService.GeBcNode(bcNodeId));
        //    return new CreateEditBcNodeViewModel
        //    {
        //        Name = bcnodeDto.Name,
        //        Description = bcnodeDto.Description,
        //        SelectedLocationId = bcnodeDto.PlaceId,
        //        Locations  =locations,
        //        BcNodeId = bcnodeDto.Id.ToString(),
        //        UserId = bcnodeDto.UserId,
        //        SelectedBcNodeId= bcnodeDto.TopBcNode,
        //        bcNodeList=bcNodeList,
        //        Group = bcnodeDto.Group,
        //        Latitude = bcnodeDto.Latitude,
        //        Longitude = bcnodeDto.Longitude
        //    };
            
        //}
        //[HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Add(CreateEditBcNodeViewModel model, bool continueEditing)
        //{
        //    model.UserId = ClaimsPrincipalExtensions.GetUserId(User);            
        //    var locationId = await _bcNodeService.AddOrUpdate(_bcNodeAdapter.ConvertDtoToBcNode(model.ModelToDto()));
        //    TempData["SuccbcNodeCreated"] = true;
        //    return RedirectToAction(nameof(Index));
        //}

        //public async Task<IActionResult> DeleteRangeBcNodeContents(IEnumerable<Guid> bcnodeContentsIds)
        //{
        //    try
        //    {
        //        await _bcNodeContentService.DeleteRange(bcnodeContentsIds);
        //        return Json(new { action = "success", title = "Info", message = "Selected contents from this bcNode has been deleted" });
        //    }
        //    catch (ArgumentException)
        //    {
        //        return Json(new { action = "error", title = "Not completed", message = "Some contents from this bcNode could not be deleted, not found " });
        //    }
        //}
        //public async Task<IActionResult> DeleteRange(IEnumerable<Guid> bcnodes)
        //{
        //    try
        //    {
        //        await _bcNodeService.DeleteRange(bcnodes);
        //        return Json(new { action = "success", title = "Info", message = "Selected bcNodes has been deleted" });
        //    }
        //    catch (ArgumentException)
        //    {
        //        return Json(new { action = "error", title = "Not completed", message = "Some bcNodes could not be deleted, not found " });
        //    }
        //}
        //[HttpGet]
        //public IActionResult EditBcNodeContent(Guid bcnodeContentId, int optionTab = 1)
        //{
        //    if (bcnodeContentId.Equals(Guid.Empty))
        //        return RedirectToAction(nameof(Index));
        //    ViewBag.Title = EditTitleContent;
        //    return View(nameof(EditBcNodeContent), BuildBcNodeContentViewModel(bcnodeContentId, optionTab));
        //}
        //[HttpGet]
        //public IActionResult Edit(Guid bcnodeId, int optionTab = 1)
        //{
        //    if (bcnodeId.Equals(Guid.Empty))
        //        return RedirectToAction(nameof(Index));
        //    ViewBag.Title = EditTitle;
        //    return View(nameof(Edit), BuildBcNodeViewModel(bcnodeId, optionTab));
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> EditBcNodeContent(BcNodeContentViewModel model)
        //{
        //    var bcnodeContentId = await _bcNodeContentService.AddOrUpdate(_bcNodeContentAdapter.ConvertDtoToBcNodeContent(model.ConvertToDto()));
        //    TempData["SuccContentUpdated"] = true;
        //    var bcnodeId = Guid.Parse(model.BcNodeId);
        //    return View(nameof(Edit), BuildBcNodeViewModel(bcnodeId));
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(CreateEditBcNodeViewModel model)
        //{
        //    var bcnodeId = await _bcNodeService.AddOrUpdate(_bcNodeAdapter.ConvertDtoToBcNode(model.ModelToDto()));
        //    TempData["SuccbcNodeUpdated"] = true;
        //    return RedirectToAction(nameof(Index));
        //}
        //[HttpPost]
        //public async Task<string>  SendConfig(Guid bcNodeId, string mode)
        //{
            
        //    if (bcNodeId.Equals(Guid.Empty))
        //        return "NoBcNodeId";

        //    //var contents = new List<Content>();
        //    //for (int i = 0; i < 3; i++)
        //    //{
        //    //    contents.Add(new Content
        //    //    {
        //    //        Type = "Type" + i,
        //    //        Bitrate = "Bitrate" + i,
        //    //        Size = "Size" + i,
        //    //        Location = "Location" + i
        //    //    });
        //    //}
        //    //var config = new ConfigMode(

        //    //    "mode1",
        //    //    Guid.NewGuid().ToString(),
        //    //    3.ToString(),
        //    //    "Success",
        //    //    contents

        //    //);
        //    var bcNode1Id = Guid.Parse("08da37e3-7835-41e9-8193-e7d5d15b48d1");
        //    var filter = new BaseFilter
        //    {
        //        RefenceId = bcNodeId,
        //        PageSize = 20
        //    };
        //    var contentDtos = _bcNodeContentAdapter.ConvertBcNodesContentToDTOs(_bcNodeContentService.GetBcNodeContents(filter));
        //    var contents = contentDtos.Select(x => new Content(x));
        //    var config = new ConfigMode(mode, Guid.NewGuid().ToString(), contents.Count().ToString(), "Process", contents);
        //    string configRequest;
        //    if (mode == "mode:2")
        //        configRequest = await new SendBcNodeConfig().SendConfig(config, bcNode1Id.ToString());
        //    else
        //        configRequest = await new SendBcNodeConfig().SendConfig(config, bcNodeId.ToString());
        //    return configRequest;
        //}


    }
}
