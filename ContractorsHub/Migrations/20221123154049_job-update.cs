using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractorsHub.Migrations
{
    public partial class jobupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_JobStatus_JobStatusId",
                table: "Jobs");

            migrationBuilder.AlterColumn<int>(
                name: "JobStatusId",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            
            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_JobStatus_JobStatusId",
                table: "Jobs",
                column: "JobStatusId",
                principalTable: "JobStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_JobStatus_JobStatusId",
                table: "Jobs");

            migrationBuilder.AlterColumn<int>(
                name: "JobStatusId",
                table: "Jobs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            
            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_JobStatus_JobStatusId",
                table: "Jobs",
                column: "JobStatusId",
                principalTable: "JobStatus",
                principalColumn: "Id");
        }
    }
}
