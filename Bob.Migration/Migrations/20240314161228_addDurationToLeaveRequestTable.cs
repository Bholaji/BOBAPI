using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bob.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class addDurationToLeaveRequestTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "LeaveRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Duration2",
                table: "LeaveRequests",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "LeaveRequests");

            migrationBuilder.DropColumn(
                name: "Duration2",
                table: "LeaveRequests");
        }
    }
}
