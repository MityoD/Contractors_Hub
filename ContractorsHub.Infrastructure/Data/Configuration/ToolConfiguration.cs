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
            List<Tool> tools = new List<Tool>()
            {
                new Tool()
                {
                    Id = 1,
                    Title = "SLIDING MITRE",
                    Brand = "EVOLUTION",
                    Quantity = 10,
                    ToolCategoryId = 3,
                    Description = "Compound mitre saw supplied with a multifunction TCT blade that can easily cut through wood, aluminium, plastic and steel, even if nails are still embedded in the material. The multipurpose blade cuts cleanly and leaves no burrs on steel. Easy to assemble and use, as the laser cutting guide helps get the highest possible accuracy. The supplied clamp also keeps the workpiece in place.",
                    OwnerId = "d6b3ac1f-4fc8-d726-83d9-6d5800ce591e",
                    IsActive = true,
                    Price = 169.99m,
                    ImageUrl = "https://media.screwfix.com/is/image//ae235?src=ae235/1062X_P&$prodImageLarge$"
                },

                new Tool()
                {
                    Id = 2,
                    Title = "COMBI DRILL",
                    Brand = "DeWALT",
                    Quantity = 20,
                    ToolCategoryId = 3,
                    Description = "Ergonomic combi drill with brushless motor and XR technology. Features 13mm metal chuck, spindle lock, rubber overmould grip and LED light for workplace illumination. Suitable for consistent screwdriving into a variety of materials with different screw sizes.",
                    OwnerId = "d6b3ac1f-4fc8-d726-83d9-6d5800ce591e",
                    IsActive = true,
                    Price = 149.99m,
                    ImageUrl = "https://media.screwfix.com/is/image//ae235?src=ae235/906KV_P&$prodImageLarge$"
                },
                
                new Tool()
                {
                    Id = 3,
                    Title = "40 PIECE SET",
                    Brand = "MAGNUSSON",
                    Quantity = 15,
                    ToolCategoryId = 1,
                    Description = "Basic tool kit for everyday household repairs. Includes screwdrivers, long nose pliers, diagonal/ side cutters, adjustable wrench, folding hex keys, tape measure, junior hacksaw, half-round file, claw hammer and magnetic torpedo level. Supplied in a Magnusson soft storage bag.",
                    OwnerId = "d6b3ac1f-4fc8-d726-83d9-6d5800ce591e",
                    IsActive = true,
                    Price = 44.99m,
                    ImageUrl = "https://media.screwfix.com/is/image//ae235?src=ae235/189PG_P&$prodImageLarge$"
                },

                new Tool()
                {
                    Id = 4,
                    Title = "DRILL BIT SET",
                    Brand = "DeWALT",
                    Quantity = 30,
                    ToolCategoryId = 2,
                    Description = "Combination set of both drill and screwdriver bits. Includes bits suitable for drilling in metal, wood, plastic and masonry along with all the most common screwdriver sizes.",
                    OwnerId = "d6b3ac1f-4fc8-d726-83d9-6d5800ce591e",
                    IsActive = true,
                    Price = 19.99m,
                    ImageUrl = "https://media.screwfix.com/is/image//ae235?src=ae235/13266_P&$prodImageLarge$"
                },

                new Tool()
                {
                    Id = 5,
                    Title = "CROSS-LINE LASER LEVEL",
                    Brand = "DeWALT",
                    Quantity = 37,
                    ToolCategoryId = 4,
                    Description = "2-button operation, out of level sensor and low battery indicator. Overmould protection maintains calibration under jobsite conditions. Supplied with carry case and protective kit box.",
                    OwnerId = "d6b3ac1f-4fc8-d726-83d9-6d5800ce591e",
                    IsActive = true,
                    Price = 119.99m,
                    ImageUrl = "https://media.screwfix.com/is/image//ae235?src=ae235/38474_P&$prodImageLarge$"
                },

                new Tool()
                {
                    Id = 6,
                    Title = "MULTIFUNCTION TESTER",
                    Brand = "KEWTECH",
                    Quantity = 12,
                    ToolCategoryId = 5,
                    Description = "Compact and robust multifunction tester. Tests 18th edition electrical installations including type A and AC RCDs. IP54 rated, even when the leads are not plugged in. Anti-trip technology loop for full no-trip loop testing of all RCD types. Low susceptibility of RCD uplift and noise interference.",
                    OwnerId = "d6b3ac1f-4fc8-d726-83d9-6d5800ce591e",
                    IsActive = true,
                    Price = 612.14m,
                    ImageUrl = "https://media.screwfix.com/is/image//ae235?src=ae235/814JY_P&$prodImageLarge$"
                },

                new Tool()
                {
                    Id = 7,
                    Title = "STORAGE TOWER",
                    Brand = "DeWALT",
                    Quantity = 33,
                    ToolCategoryId = 6,
                    Description = "Storage system that protects tools from the toughest of site and weather conditions. Polypropylene construction with a visible IP65 water and dust seal. Features easy-to-use, time-saving latches for connecting modules together and a buckled lid with lockable plastic clamps. Supplied with an extendable / detachable handle and wheels for convenient transportation. Compatible with ToughSystem.",
                    OwnerId = "d6b3ac1f-4fc8-d726-83d9-6d5800ce591e",
                    IsActive = true,
                    Price = 239.99m,
                    ImageUrl = "https://media.screwfix.com/is/image//ae235?src=ae235/718JK_P&$prodImageLarge$"
                }

             };

            return tools;
        }
    }
}
