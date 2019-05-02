using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RNews.DAL;
using RNews.Extensions;
using RNews.Models.ViewModels;
using RNews.Services;

namespace RNews.Controllers
{
    public class AuthExternalController : Controller
    {
        private UserManager<User> userManager { get; }
        private SignInManager<User> signInManager { get; }
        public AuthExternalController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
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
            User externalUser = new User
            {
                UserName = authResult.Principal.FindFirstValue(ClaimTypes.Email),
                Email = authResult.Principal.FindFirstValue(ClaimTypes.Email),
            };

            var result = await userManager.CreateAsync(externalUser);
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(externalUser, isPersistent: false);
            }

            return LocalRedirect("~/Profile");
        }
       
    }
}