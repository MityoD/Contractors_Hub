using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractorsHub.Migrations
{
    public partial class categoriesadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Tools");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Tools",
                newName: "Title");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Tools",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Tools",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "JobCategoryId",
                table: "Tools",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "JobsCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobsCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ToolsCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToolsCategories", x => x.Id);
                });

           

            migrationBuilder.InsertData(
                table: "JobsCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Heating & Plumbing" },
                    { 2, "Electrical & Lighting" },
                    { 3, "Outdoor & Gardening" },
                    { 4, "Heavy machinery" },
                    { 5, "Decorating" },
                    { 6, "Other.." }
                });

            migrationBuilder.InsertData(
                table: "ToolsCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Hand tools" },
                    { 2, "Power tool accessories" },
                    { 3, "Power tools" },
                    { 4, "Measuring tools" },
                    { 5, "Testing equipment" },
                    { 6, "Testing equipment" },
                    { 7, "Other.." }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tools_CategoryId",
                table: "Tools",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tools_JobCategoryId",
                table: "Tools",
                column: "JobCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_CategoryId",
                table: "Jobs",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_JobsCategories_CategoryId",
                table: "Jobs",
                column: "CategoryId",
                principalTable: "JobsCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tools_JobsCategories_JobCategoryId",
                table: "Tools",
                column: "JobCategoryId",
                principalTable: "JobsCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tools_ToolsCategories_CategoryId",
                table: "Tools",
                column: "CategoryId",
                principalTable: "ToolsCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_JobsCategories_CategoryId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_Tools_JobsCategories_JobCategoryId",
                table: "Tools");

            migrationBuilder.DropForeignKey(
                name: "FK_Tools_ToolsCategories_CategoryId",
                table: "Tools");

            migrationBuilder.DropTable(
                name: "JobsCategories");

            migrationBuilder.DropTable(
                name: "ToolsCategories");

            migrationBuilder.DropIndex(
                name: "IX_Tools_CategoryId",
                table: "Tools");

            migrationBuilder.DropIndex(
                name: "IX_Tools_JobCategoryId",
                table: "Tools");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_CategoryId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Tools");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Tools");

            migrationBuilder.DropColumn(
                name: "JobCategoryId",
                table: "Tools");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Tools",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Tools",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Jobs",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

           
        }
    }
}
