using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebChat.Client.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Ip { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string ConnectionId { get; set; }
        public bool IsOnline { get; set; }
    }
}
