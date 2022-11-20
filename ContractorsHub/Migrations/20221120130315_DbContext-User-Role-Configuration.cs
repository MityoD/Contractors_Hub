using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractorsHub.Migrations
{
    public partial class DbContextUserRoleConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1e62f853-4a41-4652-b9a9-8e8b236e24c7", "1fa54d33-a8ba-47b1-be64-02d6d4836152", "Administrator", "ADMINISTRATOR" },
                    { "5d937746-9833-4886-83d1-3c125ad5294c", "80196750-3beb-42a1-b321-0aa9c9f41275", "Guest", "GUEST" },
                    { "c8a8cf93-46b1-4e79-871a-1f4742a0db83", "292f8d0e-c368-4cf1-bb0a-b8cda6096ea9", "Contractor", "CONTRACTOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsContractor", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e", 0, "59b960b9-89db-45bd-8208-72445c4f557d", "guest@mail.com", false, false, false, null, "GUEST@MAIL.COM", "GUEST", "AQAAAAEAACcQAAAAEBL02Fw1MVf5VpyG3aAu/fyyLLf/RPzL/JG0D8ZWHPZRbrdwd4byJHs9lurPGzWsXA==", null, false, "3dd183ec-87e6-4c2d-b604-e58d1672e40f", false, "guest" },
                    { "d6b3ac1f-4fc8-d726-83d9-6d5800ce591e", 0, "3f56ded1-c17b-48d4-bbba-d55be26b0b77", "admin@mail.com", false, false, false, null, "ADMIN@MAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEIQTIOlz9dBZglNBfjP08+eySFiIuo0CioGb6btsBeW+4KjVz1vQa0ucwzjvLMqo7w==", null, false, "9e115bf8-4c54-40a4-823c-1f2535bf2bdf", false, "admin" },
                    { "dea12856-c198-4129-b3f3-b893d8395082", 0, "d093203c-b947-48d5-b9b6-444c42e7296f", "contractor@mail.com", false, false, false, null, "CONTRACTOR@MAIL.COM", "CONTRACTOR", "AQAAAAEAACcQAAAAEFdtAy08g4qyxjnXd+FsntIlqzg+VZwY3pnyuMXYQl5cC2fTfEK7uevf3iUwUj+uKg==", null, false, "e2ce7652-99eb-449b-a639-598099ca6816", false, "contractor" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "5d937746-9833-4886-83d1-3c125ad5294c", "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1e62f853-4a41-4652-b9a9-8e8b236e24c7", "d6b3ac1f-4fc8-d726-83d9-6d5800ce591e" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1e62f853-4a41-4652-b9a9-8e8b236e24c7", "dea12856-c198-4129-b3f3-b893d8395082" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c8a8cf93-46b1-4e79-871a-1f4742a0db83");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "5d937746-9833-4886-83d1-3c125ad5294c", "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1e62f853-4a41-4652-b9a9-8e8b236e24c7", "d6b3ac1f-4fc8-d726-83d9-6d5800ce591e" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1e62f853-4a41-4652-b9a9-8e8b236e24c7", "dea12856-c198-4129-b3f3-b893d8395082" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1e62f853-4a41-4652-b9a9-8e8b236e24c7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5d937746-9833-4886-83d1-3c125ad5294c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d6b3ac1f-4fc8-d726-83d9-6d5800ce591e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082");
        }
    }
}
