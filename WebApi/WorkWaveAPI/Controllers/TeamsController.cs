using DAL.Models;
using DAL.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkWaveAPI.ApiRequestModels;

namespace WorkWaveAPI.Controllers
{
    [DisableCors]
    [ApiController]
    [Route("api/v2/[controller]")]
    public class TeamsController : Controller
    {

        IRepository<Team> _repository;
        UserManager<User> user;
        public TeamsController(IRepository<Team> repository, UserManager<User> userManager) {
            _repository = repository;
            user = userManager;
        }

        [HttpPost]
        [Route("/createnewteam")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> CreateNewTeam(CreateTeamModel model) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var creator = await user.FindByIdAsync(model.CreatorId.ToString());
            var teamUsers = new List<User>
            {
                creator
            };

            Team newTeam = new Team()
            {
                Name = model.Name,
                Users = teamUsers
            };

            _repository.Add(newTeam);

            return Json(newTeam);
        }

        [HttpGet]
        [Route("/getuserteam")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> GetUserTeam(string userId)
        {
            var team = await _repository.GetAll().FirstOrDefaultAsync(t => t.Users.FirstOrDefault(u => u.Id == userId) != null);
            
            return team!=null ? Json(team) : NotFound();
        } 
    }
}
