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
        public async Task NameChange(string newName, string userId)
        {
            if (!String.IsNullOrWhiteSpace(newName) && !String.IsNullOrEmpty(userId))
            {
                var user = await db.People.FindAsync(userId);
                user.UserName = newName;
                user.NormalizedUserName = newName.ToUpper();
                await db.SaveChangesAsync();
                await Clients.Caller.SendAsync("NameChange", user.UserName, "ok");
            }
        }
    }
}
