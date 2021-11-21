using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TodoApp.Controllers
{
    public class TestController : Controller
    {
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Index(string logout_token) //Backchannel Logout
        {
            //TODO: implement this
            return Ok();
        }
    }
}