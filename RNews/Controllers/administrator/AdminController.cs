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
using RNews.Models.ViewModels.Admin;

namespace RNews.Controllers.administrator
{
    [Authorize]
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
        
        public async Task<IActionResult> Index()
        {
            var users = await userManager.Users.ToListAsync();
            var listModels = new List<AccountViewModel>();
            foreach (var user in users)
            {
                listModels.Add(new AccountViewModel {UserId = user.Id, UserName = user.UserName, UserEmail = user.Email, UserRole = "test" });
            }
            return View(listModels);
        }
        public async Task<IActionResult> Show(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var accountInfo = new AccountInfoViewModel
            {
                Id = user.Id,
                Name = user.UserName,
                Email = user.Email,
                Description = user.Description,
                RegisterDate = user.Created.ToShortDateString(),
                Role = "test",
                IsExternal = user.IsExternal.ToString()
            };
            return View(accountInfo);
        }
    }
}