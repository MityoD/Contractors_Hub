using ContractorsHub.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContractorsHub.Data.Configuration
{
    internal class JobCategoryConfiguration : IEntityTypeConfiguration<JobCategory>
    {
        public void Configure(EntityTypeBuilder<JobCategory> builder)
        {
            builder.HasData(CreateCategories());
        }
   
        private List<JobCategory> CreateCategories()
        {
            List<JobCategory> categories = new List<JobCategory>()
            {
                new JobCategory()
                {
                    Id = 1,
                    Name = "Heating & Plumbing"
                },

                new JobCategory()
                {
                    Id = 2,
                    Name = "Electrical & Lighting"
                },
                
                new JobCategory()
                {
                    Id = 3,
                    Name = "Outdoor & Gardening"
                },

                new JobCategory ()
                {
                    Id = 4,
                    Name = "Heavy machinery"
                },

                new JobCategory ()
                {
                    Id = 5,
                    Name = "Decorating"
                },

                new JobCategory ()
                {
                    Id = 6,
                    Name = "Other.."
                },

             };

            return categories;
        }
    }
}
