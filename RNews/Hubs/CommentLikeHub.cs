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
            var existLike = await db.CommentLikes.FirstOrDefaultAsync(c => c.CommentId == commentId);
            if (existLike == null)
            {
                var like = new CommentLike
                {
                    Comment = comment,
                    User = user,
                    IsLike = isLike
                };
                await db.CommentLikes.AddAsync(like);
                await db.SaveChangesAsync();
            }
            else
            {
                if (existLike.IsLike == true)
                {
                    existLike.IsLike = false;
                }
                else
                {
                    existLike.IsLike = true;
                }
            }
            comment.LikesCount = LikeCounter(commentId);
            await db.SaveChangesAsync();
            await Clients.All.SendAsync("CommentLikes", comment.LikesCount);
        }
        public int LikeCounter(int commentId)
        {
            var likeCounter = 0;
            var likes = db.CommentLikes.Where(c => c.CommentId == commentId).ToList();
            foreach (var like in likes)
            {
                if (like.IsLike == true)
                {
                    likeCounter++;
                }
            }
            return likeCounter;
        }
    }
}
