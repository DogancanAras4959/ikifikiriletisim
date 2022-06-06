using ikifikir.DAL.Mapping;
using ikifikir.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ikifikir.DAL
{
    public class ikifikirdbcontext : DbContext
    {
        public ikifikirdbcontext(DbContextOptions<ikifikirdbcontext> options) : base(options)
        {

        }

        public DbSet<user> users { get; set; }
        public DbSet<roles> roles { get; set; }
        public DbSet<roleUsers> rolesUsers { get; set; }
        public DbSet<teams> teams { get; set; }
        public DbSet<category> categories { get; set; }
        public DbSet<project> projects { get; set; }
        public DbSet<galleries> galleries { get; set; }
        public DbSet<videos> videos { get; set; }
        public DbSet<tagProject> tagProjects { get; set; }
        public DbSet<tags> tags { get; set; }
        public DbSet<post> posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new RoleMapping());
            modelBuilder.ApplyConfiguration(new RoleUserMapping());
            modelBuilder.ApplyConfiguration(new TeamsMapping());
            modelBuilder.ApplyConfiguration(new CategoryMapping());
            modelBuilder.ApplyConfiguration(new ProjectMapping());
            modelBuilder.ApplyConfiguration(new GalleryMapping());
            modelBuilder.ApplyConfiguration(new VideosMapping());
            modelBuilder.ApplyConfiguration(new TagMapping());
            modelBuilder.ApplyConfiguration(new tagProjectMapping());
            modelBuilder.ApplyConfiguration(new PostMapping());
            base.OnModelCreating(modelBuilder);
        }
    }
}
