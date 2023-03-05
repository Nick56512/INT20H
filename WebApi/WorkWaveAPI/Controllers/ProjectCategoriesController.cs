using DAL.Models;
using DAL.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WorkWaveAPI.Controllers
{
    [ApiController]
    [Route("api/v2/[controller]")]
    public class ProjectCategoriesController : Controller
    {
        IRepository<ProjectCategory> _repository;
        public ProjectCategoriesController(IRepository<ProjectCategory> repository) {
            _repository= repository;
        }

        [HttpGet]
        [Route("/getallcategories")]
        public ActionResult GetAllCategories()
        {
            var categories = _repository.GetAll();

            if(categories == null) 
                return NotFound();
            else
                return Json(categories);
        }

        [HttpGet]
        [Route("/getcategorybyid/{id}")]
        public async Task<ActionResult> GetCategoryById(int id)
        {
            var category = await _repository.GetByIdAsync(id);

            if (category == null)
                return NotFound();
            else
                return Json(new {categoryName=category.Name,Id=category.Id });
        }

        [HttpGet]
        [Route("/getcategorybyname/{name}")]
        public async Task<ActionResult> GetCategoryByName(string name)
        {
            var category = await _repository.GetAll().FirstOrDefaultAsync(c => c.Name == name);

            if (category == null)
                return NotFound();
            else
                return Json(new { categoryName = category.Name, Id = category.Id });
        }
    }
}
