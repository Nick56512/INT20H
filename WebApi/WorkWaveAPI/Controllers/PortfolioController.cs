using DAL.Extensions;
using DAL.Models;
using DAL.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorkWaveAPI.ApiRequestModels;
using System.IO;
using Microsoft.AspNetCore.Hosting.Server;
using WorkWaveAPI.Managers;

namespace WorkWaveAPI.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/v2/[controller]")]
    public class PortfolioController : Controller
    {
        UserManager<User> _userManager;
        IRepository<PortfolioProject> _portfolioProjectRepository;
        ProjectCategoryRepository _projectCategory;
        public PortfolioController(UserManager<User>userManager,
            IRepository<PortfolioProject>portfolio,
            ProjectCategoryRepository projectCategory) 
        {
            _userManager= userManager;
            _portfolioProjectRepository= portfolio;
            _projectCategory= projectCategory;
        }

        [HttpGet("/myPortfolio")]
        public async Task<IEnumerable<PortfolioProject>> GetMyPortfolio()
        {
            User currentUser = await _userManager.GetUserByClaimsIdentityNameAsync(User.Identity);
            return currentUser.PortfolioProjects;
        }
        [HttpDelete("/remove/{portfolioProjectId}")]
        public async Task<IActionResult> DeleteProject(int portfolioProjectId)
        {
            if (portfolioProjectId != 0) 
            {
                PortfolioProject portfolioProject = _portfolioProjectRepository.GetById(portfolioProjectId);
                if (portfolioProject != null) 
                {
                    await _portfolioProjectRepository.RemoveAsync(portfolioProject);
                    return Ok(portfolioProject);
                }
            }
            return BadRequest();
        }

        [HttpPut("/update/{portfolioProjectId}")]
        public async Task<IActionResult> UpdateProject(int portfolioProjectId)
        {
            if (portfolioProjectId != 0)
            {
                PortfolioProject portfolioProject = _portfolioProjectRepository.GetById(portfolioProjectId);
                if (portfolioProject != null)
                {
                    _portfolioProjectRepository.UpdateAsync(portfolioProject);
                    return Ok(portfolioProject);
                }
            }
            return BadRequest();
        }


        [HttpPost("/newPortfolioProject")]
        public async Task<IActionResult> AddNewPortfolioProject([FromBody]AddPortfolioProjectModel model, [FromServices]IWebHostEnvironment _host)
        {
            if (ModelState.IsValid)
            {
                User currentUser = await _userManager.GetUserByClaimsIdentityNameAsync(User.Identity);
                int id = _projectCategory.GetCategoryIdByName(model.ProjectCategoryName);

                string uploads = Path.Combine(_host.WebRootPath, "uploads");
                string filePath= await FileManager.CopyToAsync(model.PhotoFile, uploads);
                PortfolioProject portfolio = new PortfolioProject 
                { 
                    Title= model.Title,
                    ProjectCategoryId=id,
                    Description=model.Description,
                    OwnerId=currentUser.Id,
                    PhotoPath=filePath,
                    PhotoBase64=Base64Encoder.GetBase64String(model.PhotoFile),
                    Url=model.ProjectUrl
                };

                _portfolioProjectRepository.Add(portfolio);

                return Ok(portfolio);
            }
            return BadRequest();
        }
    }
}
