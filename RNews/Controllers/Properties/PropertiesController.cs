using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RNews.DAL;
using RNews.DAL.dbContext;
using RNews.Models.ViewModels;
using System.Linq;

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
        public IActionResult Properties()
        {
            var userId = UserManager.GetUserId(HttpContext.User);
            var user = db.People.Include(c => c.Posts).SingleOrDefault(c => c.Id == userId);
            return View(new PropertyViewModel
            {
                PropertyViewModelId = user.Id,
                Name = user.UserName,
                Email = user.Email
            });
        }

    }
}