using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RNews.DAL.dbContext;
using RNews.Models.ViewModels;

namespace RNews.Controllers.Search
{
    public class SearchController : Controller
    {
        private readonly ApplicationDbContext db;
        public SearchController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<IActionResult> Index(string searchString)
        {
            var posts = from p in db.Posts select p;
            if (!String.IsNullOrEmpty(searchString))
            {
                posts = posts.Where(s => s.Title.Contains(searchString));
            }
            var searchResult = await posts.ToListAsync();
            var searchModelList = new List<SearchViewModel>();
            foreach (var post in searchResult)
            {
                searchModelList.Add(new SearchViewModel
                {
                    Id = post.PostId,
                    Title = post.Title,
                    Author = post.User.UserName,
                    Description = post.Description,
                    DateOfCreated = post.Created.ToShortDateString()
                });
            }
            return View(searchModelList);
        }
    }
}