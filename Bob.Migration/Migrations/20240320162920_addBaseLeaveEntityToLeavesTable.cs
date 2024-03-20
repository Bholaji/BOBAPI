using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bob.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class addBaseLeaveEntityToLeavesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "UserTimeOffs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "UserTimeOffs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "LeaveRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "LeaveDaysAccurals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "LeaveDaysAccurals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "LeaveBalanceActivities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "LeaveBalanceActivities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "CarryOverActivities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "CarryOverActivities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_LeaveDaysAccurals_UserId_AccuralDate",
                table: "LeaveDaysAccurals",
                columns: new[] { "UserId", "AccuralDate" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_LeaveDaysAccurals_UserId_AccuralDate",
                table: "LeaveDaysAccurals");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "UserTimeOffs");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "UserTimeOffs");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "LeaveRequests");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "LeaveDaysAccurals");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "LeaveDaysAccurals");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "LeaveBalanceActivities");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "LeaveBalanceActivities");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "CarryOverActivities");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "CarryOverActivities");
        }
    }
}
