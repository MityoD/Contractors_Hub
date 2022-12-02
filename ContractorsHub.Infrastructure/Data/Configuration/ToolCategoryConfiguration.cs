using ContractorsHub.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContractorsHub.Infrastructure.Data.Configuration
{
    internal class ToolCategoryConfiguration : IEntityTypeConfiguration<ToolCategory>
    {
        public void Configure(EntityTypeBuilder<ToolCategory> builder)
        {
            builder.HasData(CreateCategories());
        }
   
        private List<ToolCategory> CreateCategories()
        {
            List<ToolCategory> categories = new List<ToolCategory>()
            {
                new ToolCategory()
                {
                    Id = 1,
                    Name = "Hand tools"
                },

                new ToolCategory()
                {
                    Id = 2,
                    Name = "Power tool accessories"
                },
                
                new ToolCategory()
                {
                    Id = 3,
                    Name = "Power tools"
                },

                new ToolCategory ()
                {
                    Id = 4,
                    Name = "Measuring tools"
                },

                new ToolCategory ()
                {
                    Id = 5,
                    Name = "Testing equipment"
                },

                new ToolCategory ()
                {
                    Id = 6,
                    Name = "Tool storage"
                },

                new ToolCategory ()
                {
                    Id = 7,
                    Name = "Other.."
                }

             };

            return categories;
        }
    }
}
