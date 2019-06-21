using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RNews.DAL;
using RNews.DAL.dbContext;
using RNews.Hubs;
using RNews.Models.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RNews.Controllers.Profile
{
    [Authorize(Roles = "admin, writer, reader")]
    public class PropertiesController : Controller
    {
        private UserManager<User> userManager { get; }
        private readonly ApplicationDbContext db;
        private readonly IHostingEnvironment appEnvironment;
        private readonly IHubContext<UserAvatarHub> hubContext;
        public PropertiesController(UserManager<User> userManager,
                                    ApplicationDbContext db,
                                    IHostingEnvironment appEnvironment,
                                    IHubContext<UserAvatarHub> hubContext)
        {
            this.db = db;
            this.userManager = userManager;
            this.appEnvironment = appEnvironment;
            this.hubContext = hubContext;
        }
       
        public async Task<IActionResult> Properties()
        {
            var userId = userManager.GetUserId(HttpContext.User);
            var user = await db.People.FirstOrDefaultAsync(c => c.Id == userId);
            var model = new PropertyViewModel
            {
                PropertyViewModelId = user.Id,
                Name = user.UserName,
                Email = user.Email,
                ImagePath = user.ImagePath,
                Description = user.Description,
                Roles = await userManager.GetRolesAsync(user)
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UploadAvatar(IFormFile uploadedFile)
        {
            var userId = userManager.GetUserId(HttpContext.User);//how to upload img to ExternalUser
            if (uploadedFile != null)
            {
                string path = "/imgs/avatars/" + userId + uploadedFile.FileName;
                using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                var user = await db.People.FindAsync(userId);
                user.ImagePath = path;
                user.ImageName = uploadedFile.FileName;
                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();
                await hubContext.Clients.All.SendAsync("UserAvatarSend", user.ImagePath);
            }
            return RedirectToAction("Properties", "Properties");
        }
    }
}