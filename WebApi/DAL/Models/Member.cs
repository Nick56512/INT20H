using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Member
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Project Project { get; set; }
    }
}
