using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bob.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class removedIsUniqueEmploeeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserEmploymentInformations_EmployeeID",
                table: "UserEmploymentInformations");

            migrationBuilder.CreateIndex(
                name: "IX_UserEmploymentInformations_EmployeeID",
                table: "UserEmploymentInformations",
                column: "EmployeeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserEmploymentInformations_EmployeeID",
                table: "UserEmploymentInformations");

            migrationBuilder.CreateIndex(
                name: "IX_UserEmploymentInformations_EmployeeID",
                table: "UserEmploymentInformations",
                column: "EmployeeID",
                unique: true);
        }
    }
}
