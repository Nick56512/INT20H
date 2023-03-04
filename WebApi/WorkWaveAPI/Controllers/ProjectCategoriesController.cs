using Microsoft.AspNetCore.Mvc;

namespace WorkWaveAPI.Controllers
{
    [ApiController]
    [Route("api/v2/[controller]")]
    public class ProjectCategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
