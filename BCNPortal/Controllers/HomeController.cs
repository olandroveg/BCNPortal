using BCNPortal.Models;
using BCNPortal.Services.ApiRequest;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace BCNPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITokenRequestService _token;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeController(ILogger<HomeController> logger,
            ITokenRequestService token,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _token = token;
            _signInManager = signInManager;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var user = _signInManager.UserManager.Users;
           // var uss = await _userManager.GetClaimsAsync(user.FirstOrDefault());
            var userId = Guid.Empty;
            // get username of the logged user
            //var us = await _userManager.GetUserNameAsync(user.FirstOrDefault());
            if (user != null && user.Count() > 0)
                userId = Guid.Parse( await _userManager.GetUserIdAsync(user.FirstOrDefault()));
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                var authenticatedUserName = httpContext.User.Identity.Name;

                // If it returns null, even when the user was authenticated, you may try to get the value of a specific claim 
                //var authenticatedUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                // var authenticatedUserId = _httpContextAccessor.HttpContext.User.FindFirst("sub").Value
                var a = 3;
                // TODO use name to set the shadow property value like in the following post: https://www.meziantou.net/2017/07/03/entity-framework-core-generate-tracking-columns
            }


            // var tok = _token.ManageToken("user", "test");
            return View();
        }
        
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