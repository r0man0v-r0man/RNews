using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using RNews.DAL.dbContext;
using RNews.Units;
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
        public async Task UserProperty(string userPropertyDescription, string userPropertyName, string userPropertyEmail, string userId)
        {
            var user = Unit.GetUser(db, userId);
            user.UserName = userPropertyName;
            user.Email = userPropertyEmail;
            user.Description = userPropertyDescription;
            Unit.SaveUser(db, user);
            await Clients.All.SendAsync("UserPropertySend", user.Description, user.UserName, user.Email );
        }
        
    }
}
