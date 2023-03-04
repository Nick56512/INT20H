using DAL.Models;
using DAL.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkWaveAPI.ApiRequestModels;

namespace WorkWaveAPI.Controllers
{
    [ApiController]
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
                var member=_membersRepository.GetById(projectMember);
                if (member != null)
                {
                    _membersRepository.Remove(member);
                    return Ok(member);
                }
            }
            return BadRequest();
        }


        [HttpPost("/addMember")]
        public async Task<IActionResult> RemoveProjectMember([FromBody] AddMemberToProjectModel model)
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
