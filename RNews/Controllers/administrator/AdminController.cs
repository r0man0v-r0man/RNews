using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RNews.DAL;
using RNews.DAL.dbContext;
using RNews.Models.ViewModels;
using RNews.Models.ViewModels.Admin;

namespace RNews.Controllers.administrator
{
    [Authorize(Roles ="admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDbContext db;
        public AdminController(UserManager<User> userManager,
                               RoleManager<IdentityRole> roleManager,
                               ApplicationDbContext db)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.db = db;
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
        public async Task<IActionResult> Categories()
        {
            var categories = await db.Categories.ToListAsync();
            var listModels = new List<AdminCategoryViewModel>();
            foreach (var category in categories)
            {
                listModels.Add(new AdminCategoryViewModel
                {
                    Name = category.Name
                });
            }
            return View(listModels);
        }
        public async Task<IActionResult> Posts()
        {
            var posts = await db.Posts.ToListAsync();
            var listModels = new List<AdminPostViewModel>();
            foreach (var post in posts)
            {
                listModels.Add(new AdminPostViewModel
                {
                    id = post.PostId,
                    Title = post.Title,
                    Author = post.User.UserName,
                    Category = post.Category.Name,
                    DateCreated = post.Created.ToShortDateString(),
                    Rating = "under construction" /*не должно быть пустым*/
                });
            }
            return View(listModels);
        }
        public async Task<IActionResult> Tags()
        {
            var tags = await db.Tags.ToListAsync();
            var listModels = new List<AdminTagViewModel>();
            foreach (var tag in tags)
            {
                listModels.Add(new AdminTagViewModel
                {
                    Id = tag.TagId,
                    Name = tag.TagName,
                    Count = tag.TagCount
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
            return View();
        }
        public async Task<IActionResult> Delete(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View();
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
            return View();
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
            return View();
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
            return View();
        }
        
    }
}