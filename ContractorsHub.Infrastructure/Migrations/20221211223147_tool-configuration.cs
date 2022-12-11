using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractorsHub.Infrastructure.Migrations
{
    public partial class toolconfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a890ca82-75f3-4f2a-bd16-56a113e35af6", "AQAAAAEAACcQAAAAEFRYSjbzVPcihRmLwWYM31HM3fEdcguOha2CmcxDeRTb4sGlpRWrrFD80472dVYEPg==", "8f12cb40-36b1-4329-88a4-5b337213cc84" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d6b3ac1f-4fc8-d726-83d9-6d5800ce591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b77b72fc-1563-4884-a888-54e8a0fb1ad4", "AQAAAAEAACcQAAAAEMAwRoTIXv0gmVFRJMl+vyRQ0UCx8woMZtj5+flQDA3rgtM9v+jc1vXJu+n+zvP2Jg==", "cd6b60bc-1e43-4288-9283-7647ed132708" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "eae24bb2-b1b0-487d-a897-b24ace483dbb", "AQAAAAEAACcQAAAAEIkmDPUXZACGDRmo7OFFpcfHDDlvZJHN2TBFmWGStfugZWgrw6FnVyv5XRkgCC0OCw==", "e0a2a457-c4e1-493f-94e1-a6be270e8b81" });

            migrationBuilder.InsertData(
                table: "Tools",
                columns: new[] { "Id", "Brand", "Description", "ImageUrl", "IsActive", "OwnerId", "Price", "Quantity", "Title", "ToolCategoryId" },
                values: new object[,]
                {
                    { 1, "EVOLUTION", "Compound mitre saw supplied with a multifunction TCT blade that can easily cut through wood, aluminium, plastic and steel, even if nails are still embedded in the material. The multipurpose blade cuts cleanly and leaves no burrs on steel. Easy to assemble and use, as the laser cutting guide helps get the highest possible accuracy. The supplied clamp also keeps the workpiece in place.", "https://media.screwfix.com/is/image//ae235?src=ae235/1062X_P&$prodImageLarge$", true, "d6b3ac1f-4fc8-d726-83d9-6d5800ce591e", 169.99m, 10, "SLIDING MITRE", 3 },
                    { 2, "DeWALT", "Ergonomic combi drill with brushless motor and XR technology. Features 13mm metal chuck, spindle lock, rubber overmould grip and LED light for workplace illumination. Suitable for consistent screwdriving into a variety of materials with different screw sizes.", "https://media.screwfix.com/is/image//ae235?src=ae235/906KV_P&$prodImageLarge$", true, "d6b3ac1f-4fc8-d726-83d9-6d5800ce591e", 149.99m, 20, "COMBI DRILL", 3 },
                    { 3, "MAGNUSSON", "Basic tool kit for everyday household repairs. Includes screwdrivers, long nose pliers, diagonal/ side cutters, adjustable wrench, folding hex keys, tape measure, junior hacksaw, half-round file, claw hammer and magnetic torpedo level. Supplied in a Magnusson soft storage bag.", "https://media.screwfix.com/is/image//ae235?src=ae235/189PG_P&$prodImageLarge$", true, "d6b3ac1f-4fc8-d726-83d9-6d5800ce591e", 44.99m, 15, "40 PIECE SET", 1 },
                    { 4, "DeWALT", "Combination set of both drill and screwdriver bits. Includes bits suitable for drilling in metal, wood, plastic and masonry along with all the most common screwdriver sizes.", "https://media.screwfix.com/is/image//ae235?src=ae235/13266_P&$prodImageLarge$", true, "d6b3ac1f-4fc8-d726-83d9-6d5800ce591e", 19.99m, 30, "DRILL BIT SET", 2 },
                    { 5, "DeWALT", "2-button operation, out of level sensor and low battery indicator. Overmould protection maintains calibration under jobsite conditions. Supplied with carry case and protective kit box.", "https://media.screwfix.com/is/image//ae235?src=ae235/38474_P&$prodImageLarge$", true, "d6b3ac1f-4fc8-d726-83d9-6d5800ce591e", 119.99m, 37, "CROSS-LINE LASER LEVEL", 4 },
                    { 6, "KEWTECH", "Compact and robust multifunction tester. Tests 18th edition electrical installations including type A and AC RCDs. IP54 rated, even when the leads are not plugged in. Anti-trip technology loop for full no-trip loop testing of all RCD types. Low susceptibility of RCD uplift and noise interference.", "https://media.screwfix.com/is/image//ae235?src=ae235/814JY_P&$prodImageLarge$", true, "d6b3ac1f-4fc8-d726-83d9-6d5800ce591e", 612.14m, 12, "MULTIFUNCTION TESTER", 5 },
                    { 7, "DeWALT", "Storage system that protects tools from the toughest of site and weather conditions. Polypropylene construction with a visible IP65 water and dust seal. Features easy-to-use, time-saving latches for connecting modules together and a buckled lid with lockable plastic clamps. Supplied with an extendable / detachable handle and wheels for convenient transportation. Compatible with ToughSystem.", "https://media.screwfix.com/is/image//ae235?src=ae235/718JK_P&$prodImageLarge$", true, "d6b3ac1f-4fc8-d726-83d9-6d5800ce591e", 239.99m, 33, "STORAGE TOWER", 6 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a8f13fac-5de8-4ea5-9a05-ec176c2902fc", "AQAAAAEAACcQAAAAEAsMxZMV+8qVcYkzALUpmCtv2c0IBcpHbKwv73oQopAMXFnviZWjflq6DfezppOJWw==", "351f9cb2-f197-4398-9694-d3a5ca902278" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d6b3ac1f-4fc8-d726-83d9-6d5800ce591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "84ba6314-5cb6-4956-a597-29573a346814", "AQAAAAEAACcQAAAAEHVXtdfkWsvm++j0gnN4S6okyrNOBp269An0uY2i2LisonniRpIvtf0iik+sMEu/nw==", "bd69e462-d36f-4f2e-b541-d9c98edecc8d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fc0bfb2b-67cc-404c-be6c-9183b3312da5", "AQAAAAEAACcQAAAAEJwdoHOodSQdkC1XbcXr6C2dmkhUj6fMT7b3A+qka5YXiSe15WLBLBT65A0q0B49vQ==", "ec364b9e-b763-4b44-a220-250eaeab928c" });
        }
    }
}
