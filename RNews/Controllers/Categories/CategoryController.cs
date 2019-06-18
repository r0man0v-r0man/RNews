using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RNews.DAL.dbContext;

namespace RNews.Controllers.Categories
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext db;
        public CategoryController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult GetCategoryPosts(string name)
        {
            if (!String.IsNullOrEmpty(name))
            {
                ViewBag.CategoryPosts = db.Posts.Where(c => c.Category.Name == name).OrderByDescending(c => c.Created).ToList();
            }
            return View();
        }
    }
}