using ApplicationCoreIdentityAzureADB2C.Web.Models;
using ApplicationCoreIdentityAzureADB2C.Web.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ApplicationCoreIdentityAzureADB2C.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IWelcomeApiService _welcomeApiService;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor contextAccessor, IWelcomeApiService welcomeApiService)
        {
            _logger = logger;            
            _contextAccessor = contextAccessor;
            _welcomeApiService = welcomeApiService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _welcomeApiService.Welcome());
            }
            catch
            {
                await _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                return View(new WelcomeMessage());
            }
        }

        public async Task<IActionResult> Logout()
        {
            await _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
