using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RNews.DAL;
using RNews.Models.ViewModels;

namespace RNews.Controllers.Profile
{
    public class PropertiesController : Controller
    {
        private UserManager<User> userManager { get; }
        public PropertiesController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }
        
        [Route("~/Properties")]
        public IActionResult Properties(ProfileViewModel model)
        {
            var userId = userManager.GetUserId(HttpContext.User);
            var user = userManager.FindByIdAsync(userId).Result;
            model.Name = user.UserName;
            model.Email = user.Email;
            return View(model);
        }
    }
}