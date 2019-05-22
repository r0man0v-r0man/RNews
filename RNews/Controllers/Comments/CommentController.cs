using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RNews.DAL;
using RNews.DAL.dbContext;
using RNews.Models.ViewModels;
using RNews.Units;

namespace RNews.Controllers.Comments
{
    public class CommentController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<User> userManager;
        public CommentController(ApplicationDbContext db, UserManager<User> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public async Task< IActionResult> Create(string content, int postId)
        {
            var userId = userManager.GetUserId(HttpContext.User);
            var user = await userManager.FindByIdAsync(userId);
            var post = Unit.GetPost(db, postId);
            var comment = new Comment
            {
                User = user,
                Content = content,
                Post = post
            };
            db.Comments.Add(comment);
            await db.SaveChangesAsync();
            return RedirectToAction("Show", "Publication", new { id = postId });
        }
        
    }
}