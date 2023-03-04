using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public virtual DbSet<ProjectCategory> ProjectCategories { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<PortfolioProject> PortfolioProjects { get; set; }
        public virtual DbSet<Chat> Chats { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Achievment> Achievments { get; set; }
        public virtual DbSet<Team> Teams { get; set; }

        public virtual DbSet<Member> Members { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
