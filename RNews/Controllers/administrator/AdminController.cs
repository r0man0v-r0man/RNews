using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RNews.DAL;
using RNews.Models.ViewModels;

namespace RNews.Controllers.administrator
{
    public class AdminController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public AdminController(UserManager<User> userManager,
                               RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var users = await userManager.Users.ToListAsync();
            var listModels = new List<AccountViewModel>();
            foreach (var user in users)
            {
                listModels.Add(new AccountViewModel { UserName = user.UserName, UserEmail = user.Email, UserRole = "test" });
            }
            return View(listModels);
        }
    }
}