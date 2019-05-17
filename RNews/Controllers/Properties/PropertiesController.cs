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
using RNews.Units;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RNews.Controllers.Profile
{
    public class PropertiesController : Controller
    {
        private UserManager<User> UserManager { get; }
        private readonly ApplicationDbContext db;
        private readonly IHostingEnvironment appEnvironment;
        private readonly IHubContext<UserAvatarHub> hubContext;
        public PropertiesController(UserManager<User> userManager,
                                    ApplicationDbContext db,
                                    IHostingEnvironment appEnvironment,
                                    IHubContext<UserAvatarHub> hubContext)
        {
            this.db = db;
            this.UserManager = userManager;
            this.appEnvironment = appEnvironment;
            this.hubContext = hubContext;
        }
        
        public IActionResult Properties()
        {
            var userId = UserManager.GetUserId(HttpContext.User);
            var user = db.People.Include(c => c.Posts).SingleOrDefault(c => c.Id == userId);
            var model = new PropertyViewModel
            {
                PropertyViewModelId = user.Id,
                Name = user.UserName,
                Email = user.Email,
                ImagePath = user.ImagePath,
                Description = user.Description
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Properties(string id)
        {
            
            var user = db.People.Include(c => c.Posts).SingleOrDefault(c => c.Id == id);
            var model = new PropertyViewModel
            {
                PropertyViewModelId = user.Id,
                Name = user.UserName,
                Email = user.Email,
                ImagePath = user.ImagePath,
                Description = user.Description
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> UploadAvatar(IFormFile uploadedFile)
        {
            var userId = UserManager.GetUserId(HttpContext.User);
            if (uploadedFile != null)
            {
                string path = "/imgs/avatars/" + uploadedFile.FileName;
                using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                var user = Unit.GetUser(db, userId);
                user.ImagePath = path;
                user.ImageName = uploadedFile.FileName;
                Unit.SaveUser(db, user);
                await hubContext.Clients.All.SendAsync("UserAvatarSend", user.ImagePath);
            }
            return RedirectToAction("Properties", "Properties");
        }
    }
}