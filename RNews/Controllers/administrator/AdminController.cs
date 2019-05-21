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
    [Authorize(Roles ="admin")]
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
                listModels.Add(new AccountViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    UserEmail = user.Email,
                    UserRoles = await userManager.GetRolesAsync(user)
                });
            }
            return View(listModels);
        }
        
        public async Task<IActionResult> Ban(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var roles = await userManager.GetRolesAsync(user);
            var result = await userManager.RemoveFromRolesAsync(user, roles);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Admin");
            }
            return RedirectToAction("Index", "Admin");
        }
        public async Task<IActionResult> SetReader(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var roles = await userManager.GetRolesAsync(user);
            if (roles != null)
            {
                await userManager.RemoveFromRolesAsync(user, roles);
            }
            var result = await userManager.AddToRoleAsync(user, "reader");
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Admin");
            }
            return RedirectToAction("Index", "Admin");
        }
        public async Task<IActionResult> SetWriter(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var roles = await userManager.GetRolesAsync(user);
            if (roles != null)
            {
                await userManager.RemoveFromRolesAsync(user, roles);
            }
            var result = await userManager.AddToRoleAsync(user, "writer");
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Admin");
            }
            return RedirectToAction("Index", "Admin");
        }
        public async Task<IActionResult> SetAdmin(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var roles = await userManager.GetRolesAsync(user);
            if (roles != null)
            {
                await userManager.RemoveFromRolesAsync(user, roles);
            }
            var result = await userManager.AddToRoleAsync(user, "admin");
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Admin");
            }
            return RedirectToAction("Index", "Admin");
        }
        

    }
}