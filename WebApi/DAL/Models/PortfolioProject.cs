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
        public string PhotoBase64 { get; set; }
        public string OwnerId { get; set; }
        public virtual User Owner { get; set; }
        public int ProjectCategoryId { get; set; }
        public virtual ProjectCategory ProjectCategory { get; set; }
    }
}
