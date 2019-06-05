using Markdig;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
            var newPost = new Post
            {
                Title = model.Title,
                Description = Unit.CreateDescription(model.Description),
                Content = model.Content,
                User = user,
                CategoryId = model.CategoryId,
                ImagePath = await Unit.UploadPostMainImageAndGetPathAsync(model.Image, appEnvironment)
            };
            await SetPostTagsAsync(GetTags(model.Tags), newPost);
            
            await db.Posts.AddAsync(newPost);
            await db.SaveChangesAsync();
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
                PostComments = post.Comments.ToList(),
                Tags = post.PostTags.ToList(),
                Rating = post.Rating
            };
            return View(showPost);
        }
        [Authorize(Roles = "admin, writer")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var post = await GetPostAsync(id);
            db.Posts.Remove(post);
            await db.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
        [HttpPost("~/upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var output = new { filename = await Unit.UploadPostMainImageAndGetPathAsync(file, appEnvironment) };
            return Json(output);
        }

        public List<Tag> GetTags(string input)
        {
            var jArray = (JArray)JsonConvert.DeserializeObject(input);

            var listInputTags = new List<Tag>();
            foreach (JObject tag in jArray)
            {
                listInputTags.Add(new Tag { TagName = (string)tag.GetValue("value") });
            }
            return listInputTags;
        }
        public async Task SetPostTagsAsync(List<Tag> tags, Post post)
        {
            foreach (var tag in tags)
            {
                var existTag = await db.Tags.FirstOrDefaultAsync(c => c.TagName == tag.TagName);
                if (existTag != null)
                {
                    existTag.TagCount++;
                    post.PostTags.Add(new PostTag { Post = post, Tag = existTag });
                }
                else
                {
                    db.Tags.Add(tag);
                    post.PostTags.Add(new PostTag { Post = post, Tag = tag });
                }
            }
        }
        public async Task<Post> GetPostAsync(int? id)
        {
            var post = await db.Posts.SingleOrDefaultAsync(p=>p.PostId == id);
            if (post != null)
            {
                return post;
            }
            else
            {
                return null;
            }
        }
        
    }
}