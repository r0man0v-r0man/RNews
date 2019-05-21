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
                Roles = await userManager.GetRolesAsync(user),
                IsExternal = user.IsExternal.ToString()
            };
            await roleManager.CreateAsync(new IdentityRole("admin"));
            return View(accountInfo);
        }
        public IActionResult Edit(string id)
        {
            return View();
        }
        public async Task<IActionResult> Edit(string id, AccountInfoViewModel model)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.Description = model.Description;
                await userManager.AddToRolesAsync(user, model.Roles);
                user.UserName = model.Name;
                var result = await userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    RedirectToAction("Index", "Admin");
                }
            }
            return View();
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
        

    }
}