using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RNews.DAL;
using RNews.DAL.dbContext;
using RNews.Models.ViewModels;

namespace RNews.Controllers
{
    public class TagsController : Controller
    {
        private readonly ApplicationDbContext db;
        public TagsController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult GetTagPosts(string name)
        {
            var result = new List<TagPostsViewModel>();
            var tag = db.Tags.FirstOrDefault(c => c.TagName == name);
           
            var news = tag.PostTags.ToList();
            foreach (var item in news)
            {
                result.Add(new TagPostsViewModel
                {
                    Id = item.Post.PostId,
                    Author = item.Post.User.UserName,
                    PostImage = item.Post.ImagePath,
                    Description = item.Post.Description,
                    Rating = item.Post.Rating,
                    Title = item.Post.Title,
                    CountOfComments = item.Post.Comments.Count(),
                    Category = item.Post.Category.Name,
                    UserImagePath = item.Post.User.ImagePath,
                    Created = item.Post.Created.ToString("d")
                });
            }

            return View(result);
        }
    }
}