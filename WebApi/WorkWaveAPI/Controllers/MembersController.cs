using DAL.Models;
using DAL.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkWaveAPI.ApiRequestModels;

namespace WorkWaveAPI.Controllers
{
    [DisableCors]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/v2/[controller]")]
    public class MembersController : Controller
    {
        UserManager<User> _userManager;
        IRepository<Member> _membersRepository;

        public MembersController(UserManager<User> userManager,IRepository<Member> memberRepository)
        {
            _userManager = userManager;
            this._membersRepository= memberRepository;
        }

        [HttpGet("/getProjectMembers/{projectId}")]
        public async Task<IActionResult> Get(int projectId)
        {
            if (projectId != 0)
            {
                var arr = _membersRepository.GetAll()
                    .Where(x => x.ProjectId == projectId);
                List<User> users = new List<User>();
                foreach (var item in arr)
                {
                    users.Add(await _userManager.FindByIdAsync(item.UserId));
                }
                return Ok(users);
            }
            return BadRequest();

        }

        [HttpDelete("/removeProjectMember/{projectMember}")]
        public async Task<IActionResult>RemoveProjectMember(int projectMember)
        {
            if(projectMember != 0)
            {
                var member= await _membersRepository.GetByIdAsync(projectMember);
                if (member != null)
                {
                    await _membersRepository.RemoveAsync(member);
                    return Ok(member);
                }
            }
            return BadRequest();
        }


        [HttpPost("/addMember")]
        public async Task<IActionResult> AddMemberToProject([FromBody] AddMemberToProjectModel model)
        {
            if (ModelState.IsValid)
            {
                string userId;
                if (string.IsNullOrEmpty(model.UserId))
                {
                    userId = (await _userManager.FindByNameAsync(model.UserName)).Id;
                }
                else userId= model.UserId;
                Member member = new Member
                {
                    UserId = userId,
                    ProjectId = model.ProjectId,
                };
                await _membersRepository.AddAsync(member);
                return Ok(member);
               
            }
            return BadRequest();
        }


    }
}
