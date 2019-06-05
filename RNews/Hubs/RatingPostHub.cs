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
    public class RatingPostHub : Hub
    {
        private readonly ApplicationDbContext db;
        public RatingPostHub(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task Rating(string ratingPost, int postId, string userId)
        {
            var post = await db.Posts.FindAsync(postId);
            var user = await db.People.FindAsync(userId);
            var existRating = await db.Ratings.Where(p => p.PostId == postId).FirstAsync(c => c.User == user);
            if (existRating == null)
            {
                var rating = new Rating
                {
                    Post = post,
                    User = user,
                    Value = Convert.ToInt32(ratingPost)
                };

                await db.Ratings.AddAsync(rating);
            }
            else
            {
                existRating.Value = Convert.ToInt32(ratingPost);
                db.Entry(existRating).State = EntityState.Modified;
            }
            post.Rating += existRating.Value;
            
            await db.SaveChangesAsync();
            await Clients.All.SendAsync("RecieveRating", existRating.Value, post.Rating);
        }

    }
}
