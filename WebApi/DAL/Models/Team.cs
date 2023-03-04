using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Project Project { get; set; }
        public virtual ICollection<User> Users { get;set; }
    }
}
