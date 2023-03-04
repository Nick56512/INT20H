using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class PortfolioProject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string PhotoPath { get; set; }
        [NotMapped]
        public string PhotoBase64 { get; set; }
        public User Owner { get; set; }
        public ProjectCategory Category { get; set; }
    }
}
