using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace RNews.Hubs
{
    public class ThemeHub : Hub
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public ThemeHub(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task SetTheme()
        {
            var theme = "";
            var currentCookie = httpContextAccessor.HttpContext.Request.Cookies["theme"];
            var oldTheme = "/lib/bootstrap/dist/css/bootstrap.css";
            var newTheme = "/css/superhero.css";

            if (currentCookie != null && currentCookie == oldTheme)
            {
                theme = newTheme;
            }
            else
            {
                theme = oldTheme;

            }
            await Clients.Caller.SendAsync("NewTheme", theme);
        }
    }
}
