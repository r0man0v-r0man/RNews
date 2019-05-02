using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RNews.Controllers.Profile
{
    public class ProfileController : Controller
    {
        [Route("~/Profile")]
        public IActionResult Profile()
        {
            return View();
        }
    }
}