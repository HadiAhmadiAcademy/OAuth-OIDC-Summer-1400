using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Diaries.Extensions;
using Diaries.Persistence;
using Microsoft.AspNetCore.Authorization;

namespace Diaries.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiariesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var userId = User.GetUserId();
            var diaries = DiariesRepository.GetAllDiariesOfUser(userId);
            return Ok(diaries);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var userId = User.GetUserId();
            var diaries = DiariesRepository.GetDiaryEntry(userId, id);
            return Ok(diaries);
        }


        [HttpGet("all")]
        public IActionResult All()
        {
            var diaries = DiariesRepository.GetAllDiaries();
            return Ok(diaries);
        }
    }
}
