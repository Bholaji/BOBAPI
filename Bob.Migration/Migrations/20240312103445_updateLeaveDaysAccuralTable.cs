using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bob.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class updateLeaveDaysAccuralTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "LeaveDaysAccurals",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "AccuralPeriod",
                table: "LeaveDaysAccurals",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AccuralDate",
                table: "LeaveDaysAccurals",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "LeaveDaysAccurals",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "AccuralPeriod",
                table: "LeaveDaysAccurals",
                type: "date",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "AccuralDate",
                table: "LeaveDaysAccurals",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
