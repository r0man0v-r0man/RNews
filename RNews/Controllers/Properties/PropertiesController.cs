using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RNews.DAL;
using RNews.DAL.dbContext;
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
        public PropertiesController(UserManager<User> userManager, ApplicationDbContext db, IHostingEnvironment appEnvironment)
        {
            this.db = db;
            this.UserManager = userManager;
            this.appEnvironment = appEnvironment;
        }

        [Route("~/Properties")]
        public IActionResult Properties(PropertyViewModel model)
        {
            var userId = UserManager.GetUserId(HttpContext.User);
            var user = db.People.Include(c => c.Posts).SingleOrDefault(c => c.Id == userId);

            model.PropertyViewModelId = user.Id;
            model.Name = user.UserName;
            model.Email = user.Email;
            model.ImagePath = user.ImagePath;
            
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
            }

            return RedirectToAction("Properties", "Properties");
            
        }


    }
}