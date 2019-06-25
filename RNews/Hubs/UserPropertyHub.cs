using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RNews.DAL;
using RNews.DAL.dbContext;
using RNews.Services;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace RNews.Hubs
{
    [Authorize]
    public class UserPropertyHub : Hub
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<User> userManager;
        public string ExeptionMessage { get; set; }
        public UserPropertyHub(ApplicationDbContext db, UserManager<User> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }
        public async Task NameChange(string newName, string userId)
        {
            if (!String.IsNullOrWhiteSpace(newName) && !String.IsNullOrEmpty(userId))
            {
                var user = await userManager.FindByIdAsync(userId);
                var result = await userManager.SetUserNameAsync(user, newName);
                if (result.Succeeded)
                {
                    await Clients.Caller.SendAsync("NameChange", user.UserName, "ok");
                }
                else
                {
                    await Clients.Caller.SendAsync("NameChange", user.UserName, "error");
                }
                
                
                
            }
        }
        public async Task DescriptionChange(string newDescription, string userId)
        {
            if (!String.IsNullOrWhiteSpace(newDescription) && !String.IsNullOrEmpty(userId))
            {
                var user = await db.People.FindAsync(userId);
                user.Description = newDescription;
                await db.SaveChangesAsync();
                await Clients.Caller.SendAsync("DescriptionChange", user.Description, "ok");
            }
        }
        
    }
}
