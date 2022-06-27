using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebChat.Client.Models;

namespace WebChat.Client.Interfaces
{
    public interface IUserService
    {
        bool Insert();
        void UpdateIp(string login, string ip);
        void RegisterConnection(string login, string connectionId);
        User GetUser(string login);
        List<User> GetUsers();
        void SetOffline(string login);

    }
}
