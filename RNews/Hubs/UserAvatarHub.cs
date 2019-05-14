using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using RNews.DAL.dbContext;
using RNews.Units;
using System.Threading.Tasks;

namespace RNews.Hubs
{
    public class UserAvatarHub : Hub
    {
        private readonly ApplicationDbContext db;
        public UserAvatarHub(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task UserAvatarRecieve(string userId)
        {
            var user = Unit.GetUser(db, userId);
            await Clients.All.SendAsync("UserAvatarSend", user.ImagePath);
        }

    }
}
