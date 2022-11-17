using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractorsHub.Data.Migrations
{
    public partial class mappingtableadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_Jobs_JobId",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_JobId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "OfferId",
                table: "Jobs");

            migrationBuilder.CreateTable(
                name: "JobOffer",
                columns: table => new
                {
                    JobId = table.Column<int>(type: "int", nullable: false),
                    OfferId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobOffer", x => new { x.JobId, x.OfferId });
                    table.ForeignKey(
                        name: "FK_JobOffer_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobOffer_Offers_OfferId",
                        column: x => x.OfferId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobOffer_OfferId",
                table: "JobOffer",
                column: "OfferId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobOffer");

            migrationBuilder.AddColumn<int>(
                name: "JobId",
                table: "Offers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OfferId",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Offers_JobId",
                table: "Offers",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_Jobs_JobId",
                table: "Offers",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
