using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bob.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class updateDateonlttoDateTimeTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "DaysRequested",
                table: "LeaveRequests",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DaysRequested",
                table: "LeaveRequests",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
