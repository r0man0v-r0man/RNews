
using RNews.DAL;
using RNews.DAL.dbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RNews.Units
{
    public static class Unit
    {
        //users
        public static IEnumerable<User> TopPublicatedUsers(ApplicationDbContext db, int count) => db.People.OrderByDescending(c => c.Posts).Take(count).ToList();
        //posts
        public static IEnumerable<Post> TopRatingPost(ApplicationDbContext db, int count) => db.Posts.OrderByDescending(c => c.Rating).Take(count).ToList();
        public static IEnumerable<Post> LastAddedPosts(ApplicationDbContext db, int count) => db.Posts.OrderByDescending(c => c.Created).Take(count).ToList();
        public static List<string> LastAddedPostsTitle(ApplicationDbContext db, int count)
        {
            var list = db.Posts.OrderByDescending(c => c.Rating).Take(count).ToList();
            var result = new List<string>();
            foreach (var item in list)
            {
                result.Add(item.Title);
            }
            return result;
        }

    }
}
