using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class AddAuthData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_Users_UserId",
                table: "RefreshToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken");

            migrationBuilder.RenameTable(
                name: "RefreshToken",
                newName: "RefreshTokens");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshToken_UserId",
                table: "RefreshTokens",
                newName: "IX_RefreshTokens_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshTokens",
                table: "RefreshTokens",
                column: "Id");

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "CreatedDate", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "user", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AuthenticatorType", "CreatedDate", "Email", "FirstName", "LastName", "PasswordHash", "PasswordSalt", "Status", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "user@user.com", "Halit", "Kalaycı", new byte[] { 170, 43, 187, 9, 183, 239, 124, 135, 34, 39, 116, 246, 15, 212, 170, 116, 6, 156, 32, 156, 179, 104, 47, 91, 68, 57, 8, 192, 24, 16, 161, 108, 227, 22, 158, 244, 229, 198, 184, 202, 12, 199, 226, 112, 245, 100, 21, 122, 125, 65, 85, 13, 108, 42, 52, 112, 8, 144, 83, 17, 141, 92, 215, 158, 236, 1, 77, 102, 132, 189, 194, 13, 171, 81, 117, 219, 237, 59, 107, 208, 205, 131, 149, 46, 255, 8, 88, 183, 197, 215, 134, 105, 19, 32, 81, 76, 38, 15, 136, 117, 57, 229, 77, 145, 123, 192, 97, 193, 88, 113, 217, 87, 47, 217, 231, 88, 65, 128, 95, 6, 22, 38, 125, 132, 203, 3, 74, 41 }, new byte[] { 221, 235, 120, 235, 105, 210, 11, 123, 177, 42, 31, 129, 27, 7, 50, 49, 156, 240, 132, 74, 195, 53, 26, 254, 69, 97, 124, 164, 246, 249, 55, 181, 225, 109, 79, 120, 135, 19, 246, 9, 170, 17, 73, 40, 38, 22, 29, 73, 131, 189, 198, 167, 105, 48, 225, 179, 152, 151, 24, 179, 126, 238, 89, 159 }, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@admin.com", "Admin", "Admin", new byte[] { 170, 43, 187, 9, 183, 239, 124, 135, 34, 39, 116, 246, 15, 212, 170, 116, 6, 156, 32, 156, 179, 104, 47, 91, 68, 57, 8, 192, 24, 16, 161, 108, 227, 22, 158, 244, 229, 198, 184, 202, 12, 199, 226, 112, 245, 100, 21, 122, 125, 65, 85, 13, 108, 42, 52, 112, 8, 144, 83, 17, 141, 92, 215, 158, 236, 1, 77, 102, 132, 189, 194, 13, 171, 81, 117, 219, 237, 59, 107, 208, 205, 131, 149, 46, 255, 8, 88, 183, 197, 215, 134, 105, 19, 32, 81, 76, 38, 15, 136, 117, 57, 229, 77, 145, 123, 192, 97, 193, 88, 113, 217, 87, 47, 217, 231, 88, 65, 128, 95, 6, 22, 38, 125, 132, 203, 3, 74, 41 }, new byte[] { 221, 235, 120, 235, 105, 210, 11, 123, 177, 42, 31, 129, 27, 7, 50, 49, 156, 240, 132, 74, 195, 53, 26, 254, 69, 97, 124, 164, 246, 249, 55, 181, 225, 109, 79, 120, 135, 19, 246, 9, 170, 17, 73, 40, 38, 22, 29, 73, 131, 189, 198, 167, 105, 48, 225, 179, 152, 151, 24, 179, 126, 238, 89, 159 }, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 });

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_Users_UserId",
                table: "RefreshTokens",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_Users_UserId",
                table: "RefreshTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshTokens",
                table: "RefreshTokens");

            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.RenameTable(
                name: "RefreshTokens",
                newName: "RefreshToken");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshToken",
                newName: "IX_RefreshToken_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_Users_UserId",
                table: "RefreshToken",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
