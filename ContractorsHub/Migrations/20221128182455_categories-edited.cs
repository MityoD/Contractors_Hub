using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractorsHub.Migrations
{
    public partial class categoriesedited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.UpdateData(
                table: "ToolsCategories",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Tool storage");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.UpdateData(
                table: "ToolsCategories",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Testing equipment");
        }
    }
}
