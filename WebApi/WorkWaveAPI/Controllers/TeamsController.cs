using Microsoft.AspNetCore.Mvc;

namespace WorkWaveAPI.Controllers
{
    [ApiController]
    [Route("api/v2/[controller]")]
    public class TeamsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
