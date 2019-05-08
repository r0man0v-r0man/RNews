using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RNews.DAL;
using RNews.DAL.dbContext;
using RNews.Models.ViewModels;

namespace RNews.Controllers.Profile
{
    public class PropertiesController : Controller
    {
        private UserManager<User> UserManager { get; }
        private readonly ApplicationDbContext db;
        public PropertiesController(UserManager<User> userManager, ApplicationDbContext db)
        {
            this.db = db;
            this.UserManager = userManager;
        }
        
        [Route("~/Properties")]
        public IActionResult Properties(ProfileViewModel model)
        {
            var userId = UserManager.GetUserId(HttpContext.User);
            var user = db.People.Include(c => c.Posts).SingleOrDefault(c => c.Id == userId);

            model.Name = user.UserName;
            model.Email = user.Email;

            foreach (var post in user.Posts)
            {
                model.Posts.Add(post.Title);
            }

            return View(model);
        }
    }
}