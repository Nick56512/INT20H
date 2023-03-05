using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class PortfolioRepository : Repository<PortfolioProject>
    {
        public PortfolioRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
