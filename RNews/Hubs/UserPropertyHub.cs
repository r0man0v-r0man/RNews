using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RNews.DAL.dbContext;
using RNews.Units;
using System;
using System.Threading.Tasks;

namespace RNews.Hubs
{
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
        
    }
}
