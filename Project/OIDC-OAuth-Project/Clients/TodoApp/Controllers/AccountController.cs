using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TodoApp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public IActionResult Logout()
        {
            return new SignOutResult(
                   new List<string>() {
                       CookieAuthenticationDefaults.AuthenticationScheme,
                       "oidc"
                   }
                );
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult PostLogoutCallback(string state)
        {
            return View();
        }


        //public async Task<IActionResult> FrontChannelLogout()
        //{
        //    await HttpContext.SignOutAsync();
        //    return Ok();
        //}

        //[AllowAnonymous]
        //[HttpPost]
        //public IActionResult BackChannelLogout(string logout_token)
        //{
        //    //LogoutSessionManager.Add(userId, user);
        //    return Ok();
        //}
    }
}