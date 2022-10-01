using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class addotpentities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailAuthenticators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ActivationKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailAuthenticators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailAuthenticators_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OtpAuthenticators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SecretKey = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtpAuthenticators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OtpAuthenticators_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 249, 224, 51, 128, 136, 127, 91, 144, 10, 117, 206, 100, 87, 83, 23, 67, 145, 154, 249, 201, 184, 240, 205, 114, 90, 250, 229, 221, 221, 181, 190, 164, 139, 85, 207, 129, 17, 122, 142, 196, 74, 64, 22, 164, 141, 248, 138, 45, 127, 124, 50, 164, 139, 179, 118, 255, 242, 162, 59, 180, 101, 73, 88, 216, 202, 140, 44, 96, 154, 28, 70, 32, 167, 27, 31, 186, 37, 97, 214, 49, 207, 184, 117, 145, 37, 163, 159, 32, 241, 121, 13, 217, 109, 195, 26, 224, 59, 226, 51, 31, 37, 222, 185, 135, 224, 252, 238, 205, 100, 104, 162, 139, 107, 240, 114, 93, 225, 183, 174, 234, 168, 28, 172, 117, 31, 193, 200, 246 }, new byte[] { 51, 152, 122, 81, 220, 151, 195, 169, 171, 125, 34, 127, 158, 64, 41, 249, 5, 236, 140, 242, 65, 252, 183, 8, 207, 248, 124, 70, 55, 218, 175, 97, 3, 57, 42, 211, 161, 206, 183, 106, 220, 223, 230, 190, 88, 41, 144, 182, 124, 64, 161, 114, 142, 245, 142, 21, 16, 250, 151, 30, 107, 208, 33, 93 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 249, 224, 51, 128, 136, 127, 91, 144, 10, 117, 206, 100, 87, 83, 23, 67, 145, 154, 249, 201, 184, 240, 205, 114, 90, 250, 229, 221, 221, 181, 190, 164, 139, 85, 207, 129, 17, 122, 142, 196, 74, 64, 22, 164, 141, 248, 138, 45, 127, 124, 50, 164, 139, 179, 118, 255, 242, 162, 59, 180, 101, 73, 88, 216, 202, 140, 44, 96, 154, 28, 70, 32, 167, 27, 31, 186, 37, 97, 214, 49, 207, 184, 117, 145, 37, 163, 159, 32, 241, 121, 13, 217, 109, 195, 26, 224, 59, 226, 51, 31, 37, 222, 185, 135, 224, 252, 238, 205, 100, 104, 162, 139, 107, 240, 114, 93, 225, 183, 174, 234, 168, 28, 172, 117, 31, 193, 200, 246 }, new byte[] { 51, 152, 122, 81, 220, 151, 195, 169, 171, 125, 34, 127, 158, 64, 41, 249, 5, 236, 140, 242, 65, 252, 183, 8, 207, 248, 124, 70, 55, 218, 175, 97, 3, 57, 42, 211, 161, 206, 183, 106, 220, 223, 230, 190, 88, 41, 144, 182, 124, 64, 161, 114, 142, 245, 142, 21, 16, 250, 151, 30, 107, 208, 33, 93 } });

            migrationBuilder.CreateIndex(
                name: "IX_EmailAuthenticators_UserId",
                table: "EmailAuthenticators",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OtpAuthenticators_UserId",
                table: "OtpAuthenticators",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailAuthenticators");

            migrationBuilder.DropTable(
                name: "OtpAuthenticators");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 170, 43, 187, 9, 183, 239, 124, 135, 34, 39, 116, 246, 15, 212, 170, 116, 6, 156, 32, 156, 179, 104, 47, 91, 68, 57, 8, 192, 24, 16, 161, 108, 227, 22, 158, 244, 229, 198, 184, 202, 12, 199, 226, 112, 245, 100, 21, 122, 125, 65, 85, 13, 108, 42, 52, 112, 8, 144, 83, 17, 141, 92, 215, 158, 236, 1, 77, 102, 132, 189, 194, 13, 171, 81, 117, 219, 237, 59, 107, 208, 205, 131, 149, 46, 255, 8, 88, 183, 197, 215, 134, 105, 19, 32, 81, 76, 38, 15, 136, 117, 57, 229, 77, 145, 123, 192, 97, 193, 88, 113, 217, 87, 47, 217, 231, 88, 65, 128, 95, 6, 22, 38, 125, 132, 203, 3, 74, 41 }, new byte[] { 221, 235, 120, 235, 105, 210, 11, 123, 177, 42, 31, 129, 27, 7, 50, 49, 156, 240, 132, 74, 195, 53, 26, 254, 69, 97, 124, 164, 246, 249, 55, 181, 225, 109, 79, 120, 135, 19, 246, 9, 170, 17, 73, 40, 38, 22, 29, 73, 131, 189, 198, 167, 105, 48, 225, 179, 152, 151, 24, 179, 126, 238, 89, 159 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 170, 43, 187, 9, 183, 239, 124, 135, 34, 39, 116, 246, 15, 212, 170, 116, 6, 156, 32, 156, 179, 104, 47, 91, 68, 57, 8, 192, 24, 16, 161, 108, 227, 22, 158, 244, 229, 198, 184, 202, 12, 199, 226, 112, 245, 100, 21, 122, 125, 65, 85, 13, 108, 42, 52, 112, 8, 144, 83, 17, 141, 92, 215, 158, 236, 1, 77, 102, 132, 189, 194, 13, 171, 81, 117, 219, 237, 59, 107, 208, 205, 131, 149, 46, 255, 8, 88, 183, 197, 215, 134, 105, 19, 32, 81, 76, 38, 15, 136, 117, 57, 229, 77, 145, 123, 192, 97, 193, 88, 113, 217, 87, 47, 217, 231, 88, 65, 128, 95, 6, 22, 38, 125, 132, 203, 3, 74, 41 }, new byte[] { 221, 235, 120, 235, 105, 210, 11, 123, 177, 42, 31, 129, 27, 7, 50, 49, 156, 240, 132, 74, 195, 53, 26, 254, 69, 97, 124, 164, 246, 249, 55, 181, 225, 109, 79, 120, 135, 19, 246, 9, 170, 17, 73, 40, 38, 22, 29, 73, 131, 189, 198, 167, 105, 48, 225, 179, 152, 151, 24, 179, 126, 238, 89, 159 } });
        }
    }
}
