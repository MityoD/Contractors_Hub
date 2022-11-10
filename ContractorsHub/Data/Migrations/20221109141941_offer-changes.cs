using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractorsHub.Data.Migrations
{
    public partial class offerchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Offer_JobId",
                table: "Offer");

            migrationBuilder.CreateIndex(
                name: "IX_Offer_JobId",
                table: "Offer",
                column: "JobId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Offer_JobId",
                table: "Offer");

            migrationBuilder.CreateIndex(
                name: "IX_Offer_JobId",
                table: "Offer",
                column: "JobId",
                unique: true);
        }
    }
}
