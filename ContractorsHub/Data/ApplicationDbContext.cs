using ContractorsHub.Data.Configuration;
using ContractorsHub.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace ContractorsHub.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Job> Jobs { get; set; }

        public DbSet<JobCategory> JobsCategories { get; set; }

        public DbSet<Tool> Tools { get; set; }

        public DbSet<ToolCategory> ToolsCategories { get; set; }

        public DbSet<Offer> Offers { get; set; }

        public DbSet<JobStatus> JobStatus { get; set; }

        public DbSet<Cart> Carts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
            builder.ApplyConfiguration(new JobStatusConfiguration());
            builder.ApplyConfiguration(new JobCategoryConfiguration());
            builder.ApplyConfiguration(new ToolCategoryConfiguration());


            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            builder.Entity<JobOffer>()
                .HasKey(x => new { x.JobId, x.OfferId });

            builder.Entity<ToolCart>()
                .HasKey(x => new { x.ToolId, x.CartId });
            //one-one ???
            //builder.Entity<ToolCart>().HasOne(t => t.Tool).WithMany(c => c.ToolsCarts).HasForeignKey(t => t.ToolId);

            //builder.Entity<ToolCart>().HasOne(t => t.Cart).WithMany(c => c.ToolsCarts).HasForeignKey(t => t.CartId);

            base.OnModelCreating(builder);
        }
    }
}