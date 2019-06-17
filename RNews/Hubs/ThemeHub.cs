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
            var currentCookie = httpContextAccessor.HttpContext.Request.Cookies["theme"];
            

            if (currentCookie == "1")
            {
                httpContextAccessor.HttpContext.Response.Cookies.Append("theme", "2");
            }
            else
            {
                httpContextAccessor.HttpContext.Response.Cookies.Append("theme", "1");

            }
            await Clients.Caller.SendAsync("NewTheme");
        }
    }
}
