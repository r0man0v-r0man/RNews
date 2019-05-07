using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RNews.DAL;
using RNews.DAL.dbContext;
using RNews.Models.ViewModels;

namespace RNews.Controllers.Publication
{
    [Authorize]
    public class PublicationController : Controller
    {
        private UserManager<User> userManager { get; }
        private ApplicationDbContext db;
        public PublicationController(UserManager<User> userManager, ApplicationDbContext db)
        {
            this.db = db;
            this.userManager = userManager;
        }
        public IActionResult Publication()
        {
            return View();
        }
        public  IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(PostViewModel model)
        {
            var userId = userManager.GetUserId(HttpContext.User);
            var user = userManager.FindByIdAsync(userId).Result;
            var newPost = new Post
            {
                Title = model.Title,
                Description = model.Description,
                Content = model.Content,
                User = user,
                CategoryId = 1
            };
            db.Posts.Add(newPost);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}