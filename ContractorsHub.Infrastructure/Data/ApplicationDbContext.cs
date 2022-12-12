using ContractorsHub.Infrastructure.Data.Configuration;
using ContractorsHub.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ContractorsHub.Infrastructure.Data
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

        public DbSet<JobOffer> JobOffer { get; set; }

        public DbSet<JobStatus> JobStatus { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Remove comment to seed the DB

            //builder.ApplyConfiguration(new UserConfiguration());
            //builder.ApplyConfiguration(new RoleConfiguration());
            //builder.ApplyConfiguration(new UserRoleConfiguration());
            //builder.ApplyConfiguration(new JobStatusConfiguration());
            //builder.ApplyConfiguration(new JobCategoryConfiguration());
            //builder.ApplyConfiguration(new ToolCategoryConfiguration());
            //builder.ApplyConfiguration(new ToolConfiguration());

            //Remove comment to seed the DB


            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            builder.Entity<JobOffer>()
                .HasKey(x => new { x.JobId, x.OfferId });

            builder.Entity<ToolCart>()
                .HasKey(x => new { x.ToolId, x.CartId });
       
            base.OnModelCreating(builder);
        }
    }
}