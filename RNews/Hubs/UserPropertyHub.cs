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
        public async Task UserPropertySend(string userPropertyName, string userId)
        {
            var user = Unit.GetUser(db, userId);
            user.UserName = userPropertyName;
            Unit.SaveUser(db, user);
            await Clients.All.SendAsync("UserProperty", user.UserName);
        }
    }
}
