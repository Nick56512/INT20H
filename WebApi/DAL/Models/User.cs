using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class User:IdentityUser
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public double WorkExperience { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string DescriptionUser { get; set; }
        public virtual ICollection<PortfolioProject> PortfolioProjects { get; set;}
        public virtual ICollection<Chat> Chats { get; set; }
        public virtual ICollection<Achievment> Achievments { get; set; }
    }
}
