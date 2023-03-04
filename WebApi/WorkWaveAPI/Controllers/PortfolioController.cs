using DAL.Extensions;
using DAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkWaveAPI.ApiRequestModels;

namespace WorkWaveAPI.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/v2/[controller]")]
    public class PortfolioController : Controller
    {
        UserManager<User> _userManager;
        public PortfolioController(UserManager<User>userManager) {
            _userManager= userManager;
        }

        [HttpGet("/myPortfolio")]
        public async Task<IActionResult> GetMyPortfolio()
        {
            User currentUser = await _userManager.GetUserByClaimsIdentityNameAsync(User.Identity);


            return View();
        }

        [HttpPost("/newPortfolioProject")]
        public async Task<IActionResult> AddNewPortfolioProject([FromBody]AddPortfolioProjectModel model)
        {
            if (ModelState.IsValid)
            {


            }
            return BadRequest();
        }
    }
}
