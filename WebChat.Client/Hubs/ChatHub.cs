using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using WebChat.Client.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebChat.Client.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IUserService _userService;
        public ChatHub(IUserService userService)
        {
            _userService = userService;
        }
        //public async Task SendMessage(string user, string message)
        //{
        //    await Clients.All.SendAsync("ReceiveMessage", user, message);
        //}

        public async Task RegisterConnection(string login)
        {
            var connectionId = Context.ConnectionId;
            _userService.RegisterConnection(login, connectionId);

        }

        public async Task<string> SendMessage(string login, string sender, string message)
        {
            var user = _userService.GetUser(login);
            await Clients.Client(user.ConnectionId).SendAsync("ReceiveMessage", message, sender);
            return message;
        }
    }
}
