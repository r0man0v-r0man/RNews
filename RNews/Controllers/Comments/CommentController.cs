using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RNews.DAL;
using RNews.DAL.dbContext;
using RNews.Hubs;
using RNews.Models.ViewModels;
using RNews.Units;

namespace RNews.Controllers.Comments
{
    public class CommentController : Controller
    {
        //может убрать? в хабе ж добавляется комментарий
        private readonly ApplicationDbContext db;
        private readonly UserManager<User> userManager;
        private readonly IHubContext<CommentHub> hubContext;

        public CommentController(ApplicationDbContext db, UserManager<User> userManager, IHubContext<CommentHub> hubContext)
        {
            this.db = db;
            this.userManager = userManager;
            this.hubContext = hubContext;
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
            //await hubContext.Clients.All.SendAsync("ContentComment", comment.Content);
            return PartialView();
        }
        
    }
}