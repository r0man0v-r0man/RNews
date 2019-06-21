using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using RNews.DAL;
using RNews.DAL.dbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RNews.Hubs
{
    [Authorize]
    public class CommentHub : Hub
    {
        private readonly ApplicationDbContext db;
        public CommentHub(ApplicationDbContext db)
        {
            this.db = db;
        }
        
        public async Task Comments(string content, int postId, string userId)
        {
            var post = await db.Posts.FindAsync(postId);
            var user = await db.People.FindAsync(userId);
            var newComment = new Comment
            {
                Content = content,
                Post = post,
                User = user
            };
            var defaultLike = new CommentLike
            {
                Comment = newComment,
                User = user,
                IsLike = false
            };
            db.CommentLikes.Add(defaultLike);
            db.Comments.Add(newComment);
            await db.SaveChangesAsync();
            await Clients.All.SendAsync("ContentComment", newComment.Content, newComment.User.UserName, newComment.Created.ToString("MM/dd/yyyy"), newComment.CommentId, user.Id, defaultLike.IsLike.ToString().ToLower());
        }
    }
}
