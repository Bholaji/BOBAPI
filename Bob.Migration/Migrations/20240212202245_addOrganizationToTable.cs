using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bob.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class addOrganizationToTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserSocials_OrganizationId",
                table: "UserSocials",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPayrolls_OrganizationId",
                table: "UserPayrolls",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFinancials_OrganizationId",
                table: "UserFinancials",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEmploymentInformations_OrganizationId",
                table: "UserEmploymentInformations",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserContact_OrganizationId",
                table: "UserContact",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAddresses_OrganizationId",
                table: "UserAddresses",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddresses_Organizations_OrganizationId",
                table: "UserAddresses",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserContact_Organizations_OrganizationId",
                table: "UserContact",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserEmploymentInformations_Organizations_OrganizationId",
                table: "UserEmploymentInformations",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFinancials_Organizations_OrganizationId",
                table: "UserFinancials",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPayrolls_Organizations_OrganizationId",
                table: "UserPayrolls",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Organizations_OrganizationId",
                table: "Users",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSocials_Organizations_OrganizationId",
                table: "UserSocials",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAddresses_Organizations_OrganizationId",
                table: "UserAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserContact_Organizations_OrganizationId",
                table: "UserContact");

            migrationBuilder.DropForeignKey(
                name: "FK_UserEmploymentInformations_Organizations_OrganizationId",
                table: "UserEmploymentInformations");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFinancials_Organizations_OrganizationId",
                table: "UserFinancials");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPayrolls_Organizations_OrganizationId",
                table: "UserPayrolls");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Organizations_OrganizationId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSocials_Organizations_OrganizationId",
                table: "UserSocials");

            migrationBuilder.DropIndex(
                name: "IX_UserSocials_OrganizationId",
                table: "UserSocials");

            migrationBuilder.DropIndex(
                name: "IX_UserPayrolls_OrganizationId",
                table: "UserPayrolls");

            migrationBuilder.DropIndex(
                name: "IX_UserFinancials_OrganizationId",
                table: "UserFinancials");

            migrationBuilder.DropIndex(
                name: "IX_UserEmploymentInformations_OrganizationId",
                table: "UserEmploymentInformations");

            migrationBuilder.DropIndex(
                name: "IX_UserContact_OrganizationId",
                table: "UserContact");

            migrationBuilder.DropIndex(
                name: "IX_UserAddresses_OrganizationId",
                table: "UserAddresses");
        }
    }
}
