using DAL.Extensions;
using DAL.Models;
using DAL.Repository;
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
    public class ProjectController : Controller
    {
        UserManager<User> _userManager;
        IRepository<Project> _projectsRepository;
        ProjectCategoryRepository _projectCategory;

        public ProjectController(UserManager<User>_userManager,IRepository<Project>projectRepository, ProjectCategoryRepository _projectCategory) {
        
            this._userManager=_userManager; 
            this._projectsRepository=projectRepository;
            this._projectCategory = _projectCategory;
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
                return Ok(project);
                
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
    }
}
