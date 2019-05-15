using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Markdig;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RNews.DAL;
using RNews.DAL.dbContext;
using RNews.Models.ViewModels;
using RNews.Units;

namespace RNews.Controllers.Publication
{
    
    public class PublicationController : Controller
    {
        private UserManager<User> UserManager { get; }
        private readonly ApplicationDbContext db;
        public PublicationController(UserManager<User> userManager, ApplicationDbContext db)
        {
            this.db = db;
            this.UserManager = userManager;
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
        public IActionResult Create(PostCreateViewModel model)
        {
            var userId = UserManager.GetUserId(HttpContext.User);
            var user = UserManager.FindByIdAsync(userId).Result;
            var img = model.Image;
            var newPost = new Post
            {
                Title = model.Title,
                Description = Unit.CreateDescription(model.Description),
                Content = model.Content,
                User = user,
                Category = model.Category
            };
            db.Posts.Add(newPost);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        
        public IActionResult Show(int id)
        {
            
            Post post = Unit.GetPost(db, id);
            var showPost = new PostShowViewModel
            {
                Title = post.Title,
                Content = Markdown.ToHtml(post.Content)
            };
            return View(showPost);
        }

        public IActionResult Delete(int id)
        {
            if (id != 0)
            {
                Unit.DeletePost(db, id);
            }
            
            return RedirectToAction("Index", "Home");
        }

    }
}