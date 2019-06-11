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
            var existRataing = user.Ratings.FirstOrDefault(c => c.PostId == postId);
            if (existRataing == null)
            {
                db.Ratings.Add(new DAL.Rating
                {
                    Post = post,
                    Value = Convert.ToInt32(ratingPost),
                    User = user
                });
                await db.SaveChangesAsync();
            }
            else
            {
                existRataing.Value = Convert.ToInt32(ratingPost);
                
            }
            post.Rating = GlobalPostRating(postId);
            await db.SaveChangesAsync();
            await Clients.All.SendAsync("RecieveRating", post.Rating);
        }

        public int GlobalPostRating(int postId)
        {
            var globalRating = 0;
            var ratings = db.Ratings.Where(p => p.PostId == postId).ToList();
            foreach (var rating in ratings)
            {
                globalRating += rating.Value;
            }
            return globalRating;
        }
    }
}
