using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RNews.DAL;
using RNews.DAL.dbContext;
using RNews.Extensions;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RNews.Controllers
{
    public class AuthExternalController : Controller
    {
        private UserManager<User> UserManager { get; }
        private SignInManager<User> SignInManager { get; }
        private readonly ApplicationDbContext db;
        public AuthExternalController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationDbContext db)
        {
            this.UserManager = userManager;
            this.SignInManager = signInManager;
            this.db = db;
        }

        public async Task<IActionResult> SignIn()
        {
            return View(await HttpContext.GetExternalProvidersAsync());
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([FromForm] string provider)
        {
            if (string.IsNullOrWhiteSpace(provider))
            {
                return BadRequest();
            }

            if (!await HttpContext.IsProviderSupportedAsync(provider))
            {
                return BadRequest();
            }


            return Challenge(new AuthenticationProperties { RedirectUri = "/LogInExternal" }, provider);
        }
        [Route("~/LogInExternal")]
        public async Task<IActionResult> LogIn()
        {
            var authResult = await HttpContext.AuthenticateAsync("Identity.External");

            var isExist = db.People
                .Where(x=>x.IsExternal == true)
                .FirstOrDefault(p => p.Email == authResult.Principal.FindFirstValue(ClaimTypes.Email));
            if (authResult.Principal.FindFirstValue(ClaimTypes.Email) == isExist.Email)
            {
                await SignInManager.SignInAsync(isExist, isPersistent: false);
                return RedirectToAction("Properties", "properties", new { id = isExist.Id});
            }
            else
            {
                User externalUser = new User
                {
                    UserName = authResult.Principal.FindFirstValue(ClaimTypes.Email),
                    Email = authResult.Principal.FindFirstValue(ClaimTypes.Email),
                    IsExternal = true
                };
                var result = await UserManager.CreateAsync(externalUser);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(externalUser, isPersistent: false);
                }
                return RedirectToAction("Properties", "properties", new { id=externalUser.Id});
            }
        }

    }
}