using Microsoft.AspNetCore.SignalR;
using RNews.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RNews.Hubs
{
    public class GeneratePasswordHub : Hub
    {
        public async Task Send()
        {
            var randomPassword = PasswordGenerator.Generate();
            await  Clients.Caller.SendAsync("sendPassword", randomPassword);
        }
    }
}
