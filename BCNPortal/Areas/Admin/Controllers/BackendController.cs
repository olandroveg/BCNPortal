using BCNPortal.Models;
using BCNPortal.Services.ApiRequest;
using BCNPortal.Services.BcnUser;
using BCNPortal.Services.Discovery;
using BCNPortal.Services.IdNRF;
using BCNPortal.Services.RegisterService;
using BCNPortal.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BCNPortal.Areas.Admin.Controllers
{
    [Area("Admin")]
    // [Authorize(Policy = "BcNode")]
    public class BackendController : Controller
    {
        
        private readonly ITokenRequestService _tokenService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IBcnUserService _bcnUserService;
        private readonly IRegisterService _registerService;
        private readonly IIdNRFService _idNRFService;
        private readonly IDiscoveryService _discoveryService;
        public BackendController(
            ITokenRequestService tokenService,
            UserManager<IdentityUser> userManager,
            IBcnUserService bcnUserService, IRegisterService registerService,
            IIdNRFService idNRFService, IDiscoveryService discoveryService
            )
        {
            
            _tokenService = tokenService;
            _userManager = userManager;
            _bcnUserService = bcnUserService;
            _registerService = registerService;
            _idNRFService = idNRFService;
            _discoveryService = discoveryService;
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

        [HttpGet]
        public async Task< IActionResult> Index()
        {
            var tokenPlusId = await GetToken();
            var portalId = await _registerService.RegisterPortal(tokenPlusId);
            await _idNRFService.AddOrUpdate(new IDinNRF { Id = portalId });
            var targetNfName = StaticConfigurationManager.AppSetting["NRFdiscoveryNF:UDRF"];
            await _discoveryService.DiscoverAllApiOfNF(tokenPlusId, targetNfName, Guid.Empty);
            return View(nameof(Index));
        }
    }
}
