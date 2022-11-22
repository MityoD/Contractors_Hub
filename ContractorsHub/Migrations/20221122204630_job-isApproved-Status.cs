using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractorsHub.Migrations
{
    public partial class jobisApprovedStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Jobs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

           
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Jobs");

            
        }
    }
}
