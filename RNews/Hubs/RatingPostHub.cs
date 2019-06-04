using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RNews.DAL.dbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RNews.Hubs
{
    public class RatingPostHub : Hub
    {
        private readonly ApplicationDbContext db;
        public RatingPostHub(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task Rating(string ratingPost, int postId)
        {
            var post = await db.Posts.FindAsync(postId);
            post.Rating += Convert.ToInt32(ratingPost);
            post.RatingCount++;
            db.Entry(post).State = EntityState.Modified;
            await db.SaveChangesAsync();
            await Clients.All.SendAsync("RecieveRating", post.Rating, post.RatingCount);
        }

    }
}
