using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class MemberRepository : Repository<Member>
    {
        public MemberRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
