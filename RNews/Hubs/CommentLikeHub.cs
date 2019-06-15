using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RNews.DAL;
using RNews.DAL.dbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RNews.Hubs
{
    public class CommentLikeHub  : Hub
    {
        private readonly ApplicationDbContext db;
        public CommentLikeHub(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task Comment(int commentId, string userId, bool isLike)
        {
            var comment = await db.Comments.FirstOrDefaultAsync(c => c.CommentId == commentId);
            var user = await db.People.FindAsync(userId);
            var existLike = await db.CommentLikes.FirstOrDefaultAsync(c => c.CommentId == commentId && c.UserId == userId);
            if (existLike == null)
            {
                var like = new CommentLike
                {
                    Comment = comment,
                    User = user,
                    IsLike = !isLike
                };
                await db.CommentLikes.AddAsync(like);
                await db.SaveChangesAsync();
               
            }
            else
            {
                existLike.IsLike = !existLike.IsLike;//update exist like
                await db.SaveChangesAsync();
            }

            comment.LikesCount = LikeCounter(commentId);
            await db.SaveChangesAsync();
            var curentUserLike = await db.CommentLikes.FirstOrDefaultAsync(c => c.Comment == comment && c.User == user);
            await Clients.All.SendAsync("CommentLikes", comment.LikesCount, curentUserLike.IsLike, commentId);
        }
        public int LikeCounter(int commentId)
        {
            var likeCounter = db.CommentLikes.Count(c => c.CommentId == commentId && c.IsLike == true);
            
            return likeCounter;
        }
    }
}
