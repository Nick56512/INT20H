using DAL.Extensions;
using DAL.Models;
using DAL.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkWaveAPI.ApiRequestModels;

namespace WorkWaveAPI.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/v2/[controller]")]
    public class ProjectController : Controller
    {
        UserManager<User> _userManager;
        IRepository<Project> _projectsRepository;
        ProjectCategoryRepository _projectCategory;
        MemberRepository _memberRepository;
        public ProjectController(UserManager<User>_userManager
            ,IRepository<Project>projectRepository
            , ProjectCategoryRepository _projectCategory,
            MemberRepository memberRepository) {
        
            this._userManager=_userManager; 
            this._projectsRepository=projectRepository;
            this._projectCategory = _projectCategory;
            this._memberRepository=memberRepository;
        }

        [HttpPost("/addNewProject")]
        public async Task<ActionResult> AddNewProject([FromBody]AddProjectModel model)
        {
            if(ModelState.IsValid)
            {
                List<int>categoriesId=new List<int>();
                foreach(var item in model.Categories)
                {
                    categoriesId.Add(_projectCategory.GetCategoryIdByName(item));
                }
                string userId = (await _userManager.GetUserByClaimsIdentityNameAsync(User.Identity)).Id;
              
                Project project = new Project
                {
                    Name = model.Name,
                    Description = model.Description,
                    IsOpen = true,
                    OwnerId =userId,
                    Categories=categoriesId.Select(x=>_projectCategory.GetById(x)).ToArray()
                };
                _projectsRepository.Add(project);
                return Ok(project.Id);
                
            }
            return BadRequest(ModelState);
        }

        [HttpPost("/closeProject/{projectId}")]
        public async Task<ActionResult> CloseProject(int projectId)
        {
            if (projectId != 0)
            {
                string userId = (await _userManager.GetUserByClaimsIdentityNameAsync(User.Identity)).Id;
                var userProject = _projectsRepository.GetAll().Where(x => x.OwnerId == userId && x.Id == projectId).First();
                if(userProject != null)
                {
                    userProject.IsOpen=false;
                    _projectsRepository.Update(userProject);
                    return Ok();
                }
            }
            return BadRequest(ModelState);
        }



        [HttpDelete("/deleteProject/{projectId}")]
        public async Task<ActionResult> DeleteProject(int projectId)
        {
            if (projectId != 0)
            {
                string userId = (await _userManager.GetUserByClaimsIdentityNameAsync(User.Identity)).Id;
                var userProject = _projectsRepository.GetAll().Where(x => x.OwnerId == userId && x.Id == projectId).First();
                if (userProject != null)
                {
                    await _projectsRepository.RemoveAsync(userProject);
                    return Ok(userProject);
                }
            }
            return BadRequest(ModelState);
        }
        [HttpGet("/getProject/{projectId}")]
        public async Task<IActionResult>Get(int projectId)
        {
            var proj=_projectsRepository.GetById(projectId);
            if(proj != null)
            {
                var projectMembers = _memberRepository.GetAll().Where(x => x.ProjectId == proj.Id);
                List<User> users = new List<User>();
                foreach (var member in projectMembers)
                {
                    users.Add(await _userManager.FindByIdAsync(member.UserId));
                }
                var categories = _projectsRepository.GetAll()
                    .Where(x => x.Id == proj.Id)
                    .Include(x => x.Categories)
                    .First();
                var res = new
                {
                    projectName = proj.Name,
                    projectDescriptio = proj.Description,
                    isOpen = proj.IsOpen,
                    rating = proj.Rating,
                    categories = categories.Categories.Select(x => x.Name),
                    members = users.ToArray(),
                    owner = proj.Owner
                };
                return Ok(res);
            }
            return BadRequest();
        }

    }
}
