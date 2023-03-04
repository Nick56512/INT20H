using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class AchievmentRepository : Repository<Achievment>
    {
        public AchievmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Achievment> GetByName(string name)
        {
            var result = await dbSet.FirstOrDefaultAsync(x => x.Name == name);
            return result!;
        }
    }
}
