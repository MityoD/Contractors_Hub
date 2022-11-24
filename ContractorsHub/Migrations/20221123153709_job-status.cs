using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractorsHub.Migrations
{
    public partial class jobstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JobStatusId",
                table: "Jobs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "JobStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobStatus", x => x.Id);
                });

            
            migrationBuilder.InsertData(
                table: "JobStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Pending" },
                    { 2, "Approved" },
                    { 3, "Declined" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_JobStatusId",
                table: "Jobs",
                column: "JobStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_JobStatus_JobStatusId",
                table: "Jobs",
                column: "JobStatusId",
                principalTable: "JobStatus",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_JobStatus_JobStatusId",
                table: "Jobs");

            migrationBuilder.DropTable(
                name: "JobStatus");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_JobStatusId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "JobStatusId",
                table: "Jobs");

           
        }
    }
}
