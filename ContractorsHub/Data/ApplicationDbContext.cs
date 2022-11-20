using ContractorsHub.Data.Configuration;
using ContractorsHub.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ContractorsHub.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Job> Jobs { get; set; }

        public DbSet<Tool> Tools { get; set; }

        public DbSet<Offer> Offers { get; set; }

        //public DbSet<JobOffer> JobsOffers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());


            builder.Entity<JobOffer>()
                .HasKey(x => new { x.JobId, x.OfferId });

            builder.Entity<Offer>().HasMany(a => a.Owner).WithOne().OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}