using ContractorsHub.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContractorsHub.Infrastructure.Data.Configuration
{
    internal class ToolConfiguration : IEntityTypeConfiguration<Tool>
    {
        public void Configure(EntityTypeBuilder<Tool> builder)
        {
            builder.HasData(CreateCategories());
        }
   
        private List<Tool> CreateCategories()
        {
            List<Tool> categories = new List<Tool>()
            {
                new Tool()
                {
                    Id = 1,
                    Title = "",
                    Brand = "",
                    Quantity = 1,
                    ToolCategoryId = ,
                    Description = "",
                    OwnerId = "",
                    IsActive = true,
                    Price = ,
                    ImageUrl = ""
                    Name = "Hand tools"
                },

                new Tool()
                {
                    Id = 2,
                       Title = "",
                    Brand = "",
                    Quantity = 1,
                    ToolCategoryId = ,
                    Description = "",
                    OwnerId = "",
                    IsActive = true,
                    Price = ,
                    ImageUrl = ""
                    Name = "Power tool accessories"
                },
                
                new Tool()
                {
                    Id = 3,
                       Title = "",
                    Brand = "",
                    Quantity = 1,
                    ToolCategoryId = ,
                    Description = "",
                    OwnerId = "",
                    IsActive = true,
                    Price = ,
                    ImageUrl = ""
                    Name = "Power tools"
                },

                new Tool()
                {
                    Id = 4,
                       Title = "",
                    Brand = "",
                    Quantity = 1,
                    ToolCategoryId = ,
                    Description = "",
                    OwnerId = "",
                    IsActive = true,
                    Price = ,
                    ImageUrl = ""
                    Name = "Measuring tools"
                },

                new Tool()
                {
                    Id = 5,
                       Title = "",
                    Brand = "",
                    Quantity = 1,
                    ToolCategoryId = ,
                    Description = "",
                    OwnerId = "",
                    IsActive = true,
                    Price = ,
                    ImageUrl = ""
                    Name = "Testing equipment"
                },

                new Tool()
                {
                    Id = 6,
                       Title = "",
                    Brand = "",
                    Quantity = 1,
                    ToolCategoryId = ,
                    Description = "",
                    OwnerId = "",
                    IsActive = true,
                    Price = ,
                    ImageUrl = ""
                    Name = "Tool storage"
                },

                new Tool()
                {
                    Id = 7,
                       Title = "",
                    Brand = "",
                    Quantity = 1,
                    ToolCategoryId = ,
                    Description = "",
                    OwnerId = "",
                    IsActive = true,
                    Price = ,
                    ImageUrl = ""
                    Name = "Other.."
                }

             };

            return categories;
        }
    }
}
