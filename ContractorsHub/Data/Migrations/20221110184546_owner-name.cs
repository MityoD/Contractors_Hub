using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractorsHub.Data.Migrations
{
    public partial class ownername : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerName",
                table: "Jobs",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerName",
                table: "Jobs");
        }
    }
}
