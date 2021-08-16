using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using MvcClient.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MvcClient.Controllers
{
    [Authorize]
    public class TokenInfoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
