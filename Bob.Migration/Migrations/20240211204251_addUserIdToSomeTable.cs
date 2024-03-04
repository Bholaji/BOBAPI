using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bob.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class addUserIdToSomeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserEmploymentInformations_Users_UserId",
                table: "UserEmploymentInformations");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFinancials_Users_UserId",
                table: "UserFinancials");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPayrolls_Users_UserId",
                table: "UserPayrolls");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSocials_Users_UserId",
                table: "UserSocials");

            migrationBuilder.DropIndex(
                name: "IX_UserSocials_UserId",
                table: "UserSocials");

            migrationBuilder.DropIndex(
                name: "IX_UserPayrolls_UserId",
                table: "UserPayrolls");

            migrationBuilder.DropIndex(
                name: "IX_UserFinancials_UserId",
                table: "UserFinancials");

            migrationBuilder.DropIndex(
                name: "IX_UserEmploymentInformations_UserId",
                table: "UserEmploymentInformations");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "UserSocials",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "UserPayrolls",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "UserFinancials",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "UserEmploymentInformations",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_UserSocials_UserId",
                table: "UserSocials",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserPayrolls_UserId",
                table: "UserPayrolls",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserFinancials_UserId",
                table: "UserFinancials",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserEmploymentInformations_UserId",
                table: "UserEmploymentInformations",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_UserEmploymentInformations_Users_UserId",
                table: "UserEmploymentInformations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFinancials_Users_UserId",
                table: "UserFinancials",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPayrolls_Users_UserId",
                table: "UserPayrolls",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSocials_Users_UserId",
                table: "UserSocials",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserEmploymentInformations_Users_UserId",
                table: "UserEmploymentInformations");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFinancials_Users_UserId",
                table: "UserFinancials");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPayrolls_Users_UserId",
                table: "UserPayrolls");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSocials_Users_UserId",
                table: "UserSocials");

            migrationBuilder.DropIndex(
                name: "IX_UserSocials_UserId",
                table: "UserSocials");

            migrationBuilder.DropIndex(
                name: "IX_UserPayrolls_UserId",
                table: "UserPayrolls");

            migrationBuilder.DropIndex(
                name: "IX_UserFinancials_UserId",
                table: "UserFinancials");

            migrationBuilder.DropIndex(
                name: "IX_UserEmploymentInformations_UserId",
                table: "UserEmploymentInformations");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "UserSocials",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "UserPayrolls",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "UserFinancials",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "UserEmploymentInformations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserSocials_UserId",
                table: "UserSocials",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserPayrolls_UserId",
                table: "UserPayrolls",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserFinancials_UserId",
                table: "UserFinancials",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserEmploymentInformations_UserId",
                table: "UserEmploymentInformations",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserEmploymentInformations_Users_UserId",
                table: "UserEmploymentInformations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFinancials_Users_UserId",
                table: "UserFinancials",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPayrolls_Users_UserId",
                table: "UserPayrolls",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSocials_Users_UserId",
                table: "UserSocials",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
