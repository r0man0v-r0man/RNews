using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Markdig;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IHostingEnvironment appEnvironment;
        public PublicationController(UserManager<User> userManager, ApplicationDbContext db, IHostingEnvironment appEnvironment)
        {
            this.db = db;
            this.UserManager = userManager;
            this.appEnvironment = appEnvironment;
        }
        
        public  IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(PostCreateViewModel model)
        {
            var userId = UserManager.GetUserId(HttpContext.User);
            var user = UserManager.FindByIdAsync(userId).Result;
            var newPost = new Post
            {
                Title = model.Title,
                Description = Unit.CreateDescription(model.Description),
                Content = model.Content,
                User = user,
                Category = model.Category,
                ImagePath = await Unit.UploadPostMainImageAndGetPathAsync(model.Image, appEnvironment)
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
                DateOfCreatedAuthor = post.User.Created,
                AuthorName = post.User.UserName,
                AuthorAvatar = post.User.ImagePath,
                Title = post.Title,
                ImagePath = post.ImagePath,
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
        [HttpPost("~/upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var output = new { filename = await Unit.UploadPostMainImageAndGetPathAsync(file, appEnvironment) };
            var filename = await Unit.UploadPostMainImageAndGetPathAsync(file, appEnvironment);
            return Json(output);
        }
        
    }
}