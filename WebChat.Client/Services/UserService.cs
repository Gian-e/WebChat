using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebChat.Client.Interfaces;
using WebChat.Client.Models;

namespace WebChat.Client.Services
{
    public class UserService : IUserService
    {
        private Context context;
        public UserService(Context _context)
        {
            context = _context;
        }

        public List<User> GetUsers()
        {
            return context.Users.ToList();
        }

        public void SetOffline(string login)
        {
            var user = context.Users.Where(x => x.Login.ToUpper() == login.ToUpper()).FirstOrDefault();
            user.IsOnline = false;
            context.SaveChanges();
        }

        public bool Insert()
        {
            context.Users.Add(new User() { Ip = "sdfsd", Login = "dsada", Password = "fsdfdsf" });
            context.SaveChanges();
            var a = context.Users.ToList();
            return true;
        }

        public void RegisterConnection(string login, string connectionId)
        {
            var user = context.Users.Where(x => x.Login.ToUpper() == login.ToUpper()).FirstOrDefault();
            user.ConnectionId = connectionId;
            user.IsOnline = true;
            context.SaveChanges();
        }

        public void UpdateIp(string login, string ip)
        {
            var user = context.Users.Where(x => x.Login.ToUpper() == login.ToUpper()).FirstOrDefault();
            if (user == null)
            {
                user = new User()
                {
                    Login = login,
                    Ip = ip
                };
                context.Users.Add(user);
            }
            else
            {
                user.Ip = ip;
                context.Users.Update(user);
            }
            context.SaveChanges();
            return;
        }

        public User GetUser(string login)
        {
            return context.Users.Where(x => x.Login.ToUpper() == login.ToUpper()).FirstOrDefault();
        }
    }
}
