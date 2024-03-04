using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bob.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class addTaskJobToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaskDescription",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "TaskList",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "TaskName",
                table: "Tasks");

            migrationBuilder.AddColumn<Guid>(
                name: "TaskJobId",
                table: "Tasks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "TaskJobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TaskDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TaskList = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskJobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskJobs_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TaskJobId",
                table: "Tasks",
                column: "TaskJobId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskJobs_OrganizationId",
                table: "TaskJobs",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_TaskJobs_TaskJobId",
                table: "Tasks",
                column: "TaskJobId",
                principalTable: "TaskJobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_TaskJobs_TaskJobId",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "TaskJobs");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_TaskJobId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "TaskJobId",
                table: "Tasks");

            migrationBuilder.AddColumn<string>(
                name: "TaskDescription",
                table: "Tasks",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TaskList",
                table: "Tasks",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TaskName",
                table: "Tasks",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
