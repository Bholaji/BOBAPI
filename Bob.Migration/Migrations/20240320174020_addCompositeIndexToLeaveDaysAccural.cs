using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bob.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class addCompositeIndexToLeaveDaysAccural : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LeaveDaysAccurals_UserId_AccuralDate",
                table: "LeaveDaysAccurals");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveDaysAccurals_UserId_AccuralDate_ActivityType",
                table: "LeaveDaysAccurals",
                columns: new[] { "UserId", "AccuralDate", "ActivityType" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LeaveDaysAccurals_UserId_AccuralDate_ActivityType",
                table: "LeaveDaysAccurals");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveDaysAccurals_UserId_AccuralDate",
                table: "LeaveDaysAccurals",
                columns: new[] { "UserId", "AccuralDate" },
                unique: true);
        }
    }
}
