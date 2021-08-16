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
    public class DiariesController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var content = await client.GetStringAsync("https://localhost:5005/api/diaries");

            var model = JsonConvert.DeserializeObject<List<Diary>>(content);
            return View(model);
        }
    }
}
