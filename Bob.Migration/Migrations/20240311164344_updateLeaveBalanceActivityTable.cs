using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bob.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class updateLeaveBalanceActivityTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActivityType",
                table: "LeaveBalanceActivities",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivityType",
                table: "LeaveBalanceActivities");
        }
    }
}
