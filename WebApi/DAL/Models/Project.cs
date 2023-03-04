using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsOpen { get; set; }
        public double Rating { get; set; }
        public string OwnerId { get; set; }
        public virtual User Owner { get; set; }
        public virtual ICollection<ProjectCategory> Categories { get; set;}
    }
}
