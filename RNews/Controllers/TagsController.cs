using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RNews.DAL;
using RNews.DAL.dbContext;

namespace RNews.Controllers
{
    public class TagsController : Controller
    {
        private readonly ApplicationDbContext db;
        public TagsController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index(string name)
        {
            List<Post> result = new List<Post>();
            var tag = db.Tags.FirstOrDefault(c => c.TagName == name);

            

            return View();
        }
    }
}