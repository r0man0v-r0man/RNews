﻿using Markdig;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RNews.DAL;
using RNews.DAL.dbContext;
using RNews.Models.ViewModels;
using RNews.Units;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RNews.Controllers.Publication
{
    [Authorize(Roles = "admin, writer")]
    public class PublicationController : Controller
    {
        private UserManager<User> userManager { get; }
        private readonly ApplicationDbContext db;
        private readonly IHostingEnvironment appEnvironment;
        public PublicationController(UserManager<User> userManager, ApplicationDbContext db, IHostingEnvironment appEnvironment)
        {
            this.db = db;
            this.userManager = userManager;
            this.appEnvironment = appEnvironment;
        }

        public IActionResult Create()
        {
            var model = new PostCreateViewModel
            {
                Categories = db.Categories
                                  .Select(a => new SelectListItem()
                                  {
                                      Value = a.CategoryId.ToString(),
                                      Text = a.Name
                                  })
                                  .ToList()
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Create(PostCreateViewModel model)
        {
            var userId = userManager.GetUserId(HttpContext.User);
            var user = userManager.FindByIdAsync(userId).Result;
            var postTags = (JArray) JsonConvert.DeserializeObject(model.Tags);
            foreach (JObject item in postTags)
            {
                string name = (string)item.GetValue("value");
            }
            var newPost = new Post
            {
                Title = model.Title,
                Description = Unit.CreateDescription(model.Description),
                Content = model.Content,
                User = user,
                CategoryId = model.CategoryId,
                ImagePath = await Unit.UploadPostMainImageAndGetPathAsync(model.Image, appEnvironment)
            };

            db.Posts.Add(newPost);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        [AllowAnonymous]
        public IActionResult Show(int id)
        {
            ViewBag.CurrentUserId = userManager.GetUserId(HttpContext.User);

            Post post = Unit.GetPost(db, id);
            var showPost = new PostShowViewModel
            {
                PostId = post.PostId,
                DateOfCreatedAuthor = post.User.Created,
                AuthorName = post.User.UserName,
                AuthorAvatar = post.User.ImagePath,
                Title = post.Title,
                ImagePath = post.ImagePath,
                Content = Markdown.ToHtml(post.Content),
                PostComments = post.Comments.ToList()
            };
            return View(showPost);
        }
        [Authorize(Roles = "admin, writer")]
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
            return Json(output);
        }

       
    }
}