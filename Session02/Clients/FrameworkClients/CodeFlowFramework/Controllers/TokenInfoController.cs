using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeFlowFramework.Controllers
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
