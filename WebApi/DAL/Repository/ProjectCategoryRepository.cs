using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class ProjectCategoryRepository : Repository<ProjectCategory>
    {
        public ProjectCategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public int GetCategoryIdByName(string name)
        {
            return dbSet.Where(x=>x.Name==name).First().Id;
        }
    }
}
