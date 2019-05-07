using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using RNews.DAL;
using RNews.DAL.dbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RNews.Units
{
    public interface IUnit
    {

        Post GetPost(int id);
        void AddPost(Post post);
        IEnumerable<Post> TopRatingPosts(int count);
        IEnumerable<string> TopRatingPostsTitle(int count);

        IEnumerable<Category> GetCategories();
        

        void Complete();
    }
}
