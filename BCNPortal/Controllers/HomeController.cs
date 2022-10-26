using BCNPortal.Data;
using BCNPortal.Models;
using BCNPortal.Services.ApiRequest;
using BCNPortal.Services.BcnUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace BCNPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITokenRequestService _tokenService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IBcnUserService _bcnUserService;

        public HomeController(ILogger<HomeController> logger,
            ITokenRequestService tokenService,
            UserManager<IdentityUser> userManager,
            IBcnUserService bcnUserService
            )
        {
            _logger = logger;
            _tokenService = tokenService;
            _userManager = userManager;
            _bcnUserService = bcnUserService;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        //private async Task<string> GetToken()
        //{
        //    // get username of the logged user
        //    var userId = Guid.Empty;
        //    var bcnPassword = "";
        //    var bcnUsername = "";
        //    string token = "";
        //    var loggedUser = await _userManager.GetUserAsync(HttpContext.User);
        //    if (loggedUser != null)
        //    {
        //        userId = Guid.Parse(await _userManager.GetUserIdAsync(loggedUser));
        //        var bcnUser2 = _bcnUserService.GetBcnUserAccountByUserPortalId(userId);
        //        bcnPassword = bcnUser2.BcnPassword;
        //        bcnUsername = bcnUser2.BcnUsername;

        //    }
        //    if (bcnUsername != "" && bcnPassword != "")
        //        token = await _tokenService.ManageToken(bcnUsername, bcnPassword, userId);
        //    return token;
        //}
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}