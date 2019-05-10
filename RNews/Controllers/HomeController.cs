using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RNews.DAL.dbContext;
using RNews.Hubs;
using RNews.Models;
using RNews.Units;

namespace RNews.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IHubContext<RNewsCRUDHub> hubContext;
        public HomeController(ApplicationDbContext db, IHubContext<RNewsCRUDHub> hubContext)
        {
            this.hubContext = hubContext;
            this.db = db;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.LastAdded = Unit.LastAddedPosts(db, 6);
            ViewBag.TopRatingPost = Unit.TopRatingPost(db, 6);
            await hubContext.Clients.All.SendAsync("Notify", $"Home page loaded at: {DateTime.Now}");
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
    }
}
