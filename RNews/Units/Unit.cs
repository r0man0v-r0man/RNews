
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RNews.DAL;
using RNews.DAL.dbContext;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RNews.Units
{
    public static class Unit
    {
        public static User GetUser(ApplicationDbContext db, string id) => db.People.Find(id);
        public static void SaveUser(ApplicationDbContext db, User user)
        {
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }
        //posts
        public static IEnumerable<Post> TopRatingPost(ApplicationDbContext db, int count) => db.Posts.OrderByDescending(c => c.Rating).Take(count).ToList();
        public static IEnumerable<Post> LastAddedPosts(ApplicationDbContext db, int count) => db.Posts.OrderByDescending(c => c.Created).Take(count).ToList();
        public static List<string> LastAddedPostsTitle(ApplicationDbContext db, int count)
        {
            var list = db.Posts.OrderByDescending(c => c.Created).Take(count).ToList();
            var result = new List<string>();
            foreach (var item in list)
            {
                result.Add(item.Title);
            }
            return result;
        }
        public static void DeletePost(ApplicationDbContext db, int id)
        {
            Post post = Unit.GetPost(db, id);
            db.Posts.Remove(post);
            db.SaveChanges();
        }
        public static Post GetPost(ApplicationDbContext db, int id)
        {
            Post post = db.Posts.Find(id);
            if (post != null)
            {
                return post;
            }
            else
            {
                return null;
            }
        }
        public static string CreateDescription(string descriptionText)
        {
            string[] temp = descriptionText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var DescriptionLength = 14;
            string tempDescription = "";
            if (temp.Length <= DescriptionLength)
            {
                for (int i = 0; i < temp.Length; i++)
                {
                    tempDescription += " " + temp[i];
                }
                return String.Concat(tempDescription, "...");
            }
            else
            {
                for (int i = 0; i < DescriptionLength; i++)
                {
                    tempDescription += " " + temp[i];
                }
                return String.Concat(tempDescription, "...");
            }
            
        }
        public static async Task<string> UploadPostMainImageAndGetPathAsync(IFormFile file, IHostingEnvironment appEnvironment)
        {
            
            if (file != null)
            {
                string path = "/imgs/posts/" + file.FileName;
                using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                return path;
            }
            else
            {
                return "test";
            }
            
        }
    }
}
