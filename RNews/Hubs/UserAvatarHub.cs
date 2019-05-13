using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using RNews.DAL.dbContext;
using RNews.Units;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RNews.Hubs
{
    public class UserAvatarHub : Hub
    {
        private readonly ApplicationDbContext db;
        private readonly IHostingEnvironment appEnvironment;
        public UserAvatarHub(ApplicationDbContext db, IHostingEnvironment appEnvironment)
        {
            this.db = db;
            this.appEnvironment = appEnvironment;
        }
        public async Task UserAvatarRecieve( string userId)
        {
            //if (uploadedFile != null)
            //{
            //    string path = "/imgs/avatars/" + uploadedFile.FileName;
            //    using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
            //    {
            //        await uploadedFile.CopyToAsync(fileStream);
            //    }
                var user = Unit.GetUser(db, userId);
                //user.ImagePath = path;
                //user.ImageName = uploadedFile.FileName;
                Unit.SaveUser(db, user);
                await Clients.All.SendAsync("UserAvatarSend", user.ImagePath);
            //}
        }
        //public async Task UserAvatarRecieve(IFormFile uploadedFile, string userId)
        //{
        //    if (uploadedFile != null)
        //    {
        //        string path = "/imgs/avatars/" + uploadedFile.FileName;
        //        using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
        //        {
        //            await uploadedFile.CopyToAsync(fileStream);
        //        }
        //        var user = Unit.GetUser(db, userId);
        //        user.ImagePath = path;
        //        user.ImageName = uploadedFile.FileName;
        //        Unit.SaveUser(db, user);
        //        await Clients.All.SendAsync("UserAvatarSend", user.ImagePath);
        //    }
        //}

    }
}
