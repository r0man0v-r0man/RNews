using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var currentTheme = httpContextAccessor.HttpContext.Request.Cookies["theme"];
            var oldTheme = "/lib/bootstrap/dist/css/bootstrap.css";
            var newTheme = "/css/superhero.css";
            if (currentTheme==null)
            {

                httpContextAccessor.HttpContext.Response.Cookies.Append("theme", oldTheme);
            }
            
            if (currentTheme != newTheme)
            {
                httpContextAccessor.HttpContext.Response.Cookies.Append("theme", newTheme);
            }
            if (currentTheme != oldTheme)
            {
                httpContextAccessor.HttpContext.Response.Cookies.Append("theme", newTheme);
            }
            await Clients.Caller.SendAsync("NewTheme", newTheme);
        }
    }
}
