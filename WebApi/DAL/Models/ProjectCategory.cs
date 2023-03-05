using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ProjectCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}
