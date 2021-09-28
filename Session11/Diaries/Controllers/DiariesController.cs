using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diaries.Extensions;
using Diaries.Persistence;
using Diaries.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Diaries.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiariesController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var token = HttpContext.Request.GetAccessToken();
            var result = await TokenIntrospectionService.Introspect(token);

            var userId = User.GetUserId();
            var diaries = DiariesRepository.GetAllDiariesOfUser(userId);
            return Ok(diaries);
        }
    }
}
