using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
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
        public UserPropertyHub(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task UserProperty(string userPropertyDescription, string userPropertyName, string userId)
        {
            if (!String.IsNullOrEmpty(userPropertyDescription) && !String.IsNullOrEmpty(userPropertyName))
            {
                var user = await db.People.FindAsync(userId);
                user.UserName = userPropertyName;
                user.Description = userPropertyDescription;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                await Clients.All.SendAsync("UserPropertySend", user.Description, user.UserName);
            }
            
        }
        public async Task EmailChange(string newEmail, string userId)
        {
            if (!String.IsNullOrWhiteSpace(newEmail) && !String.IsNullOrEmpty(userId))
            {
                var user = await db.People.FindAsync(userId);
                if (RegexUtilities.IsValidEmail(newEmail))
                {
                    user.Email = newEmail;
                    await db.SaveChangesAsync();
                    await Clients.Caller.SendAsync("EmailChange", user.Email);
                }
                else
                {
                    await Clients.Caller.SendAsync("EmailChange", user.Email);
                }
            }
        }
    }
}
