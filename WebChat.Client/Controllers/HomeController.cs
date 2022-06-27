using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebChat.Client.Interfaces;
using WebChat.Client.Models;

namespace WebChat.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private const string loginCookieName = "login";

        public HomeController(ILogger<HomeController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public override void OnActionExecuting(ActionExecutingContext ctx)
        {
            string login = Request.Cookies[loginCookieName];
            if (string.IsNullOrWhiteSpace(login))
            {
                ctx.Result = new RedirectToRouteResult(
                                    new RouteValueDictionary(
                                       new { action = "Index", controller = "Login" }));
            }
        }

        public IActionResult Index()
        {
            var users = _userService.GetUsers();
            return View(users);
        }

        [HttpGet]
        public IActionResult OpenChat(string login)
        {
            var user = _userService.GetUser(login);
            return View("Chat", user);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
