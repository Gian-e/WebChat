using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebChat.Client.Interfaces;

namespace WebChat.Client.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        private const int loginExpireTime = 60;
        private const string loginCookieName = "login";
        private const string ipCookieName = "ipaddr";

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Logout()
        {
            string login = Request.Cookies[loginCookieName];
            Response.Cookies.Delete(loginCookieName);
            Response.Cookies.Delete(ipCookieName);
            _userService.SetOffline(login);
            return View("Index");
        }

        [HttpPost]
        public void Login(string login)
        {

            string clientIPAddr = HttpContext.Connection.RemoteIpAddress?.ToString();

            _userService.UpdateIp(login, clientIPAddr);

            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddMinutes(loginExpireTime);

            Response.Cookies.Append(loginCookieName, login, option);
            Response.Cookies.Append(ipCookieName, clientIPAddr, option);
        }

    }
}
