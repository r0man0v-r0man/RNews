using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RNews.DAL.dbContext;

namespace RNews.Controllers.Publication
{
    [Authorize]
    public class PublicationController : Controller
    {
        public ApplicationDbContext db { get; set; }
        public IActionResult Publication()
        {
            return View();
        }
    }
}