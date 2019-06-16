using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RNews.Controllers.Theme
{
    public class ThemeController : Controller
    {
        public IActionResult Toggle()
        {
            string oldTheme = "/lib/bootstrap/dist/css/bootstrap.css";
            string newTheme = "/css/superhero.css";
            if (Request.Cookies["theme"] == null)
            {
                Response.Cookies.Append("theme", oldTheme);
            }
            if (Request.Cookies["theme"] != null && Request.Cookies["theme"] == oldTheme)
            {
                Response.Cookies.Append("theme", newTheme);
            }
            else
            {
                Response.Cookies.Append("theme", oldTheme);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}