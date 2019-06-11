using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RNews.DAL;
using RNews.DAL.dbContext;
using RNews.Hubs;
using RNews.Models;
using RNews.Models.ViewModels;
using RNews.Units;

namespace RNews.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext db;
        public HomeController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.LastAdded = LastAddedPosts(3);
            ViewBag.TopRatingPost = TopRatingPost(3);

            ViewBag.Tags = await db.Tags.OrderBy(c => c.TagName).ToListAsync();
            return View();
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IEnumerable<Post> TopRatingPost(int count) => db.Posts.OrderByDescending(c => c.Rating).Take(count).ToList();
        public IEnumerable<Post> LastAddedPosts(int count) => db.Posts.OrderByDescending(c => c.Created).Take(count).ToList();
    }
}
