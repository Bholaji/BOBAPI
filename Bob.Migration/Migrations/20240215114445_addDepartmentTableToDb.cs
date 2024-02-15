using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bob.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class addDepartmentTableToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Contract",
                table: "UserEmploymentInformations");

            migrationBuilder.RenameColumn(
                name: "RoleName",
                table: "Roles",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "PermissionName",
                table: "Permissions",
                newName: "Name");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                table: "UserEmploymentInformations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "EmployeeID",
                table: "UserEmploymentInformations",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EmploymentContract",
                table: "UserEmploymentInformations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmploymentType",
                table: "UserEmploymentInformations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "JobTtle",
                table: "UserEmploymentInformations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserFinancials_AccountNumber",
                table: "UserFinancials",
                column: "AccountNumber",
                unique: true,
                filter: "[AccountNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserEmploymentInformations_DepartmentId",
                table: "UserEmploymentInformations",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEmploymentInformations_EmployeeID",
                table: "UserEmploymentInformations",
                column: "EmployeeID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserContact_MobileNumber",
                table: "UserContact",
                column: "MobileNumber",
                unique: true,
                filter: "[MobileNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserContact_NationalId",
                table: "UserContact",
                column: "NationalId",
                unique: true,
                filter: "[NationalId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserContact_PassportNumber",
                table: "UserContact",
                column: "PassportNumber",
                unique: true,
                filter: "[PassportNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserContact_PersonalEmail",
                table: "UserContact",
                column: "PersonalEmail",
                unique: true,
                filter: "[PersonalEmail] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserContact_PhoneNumber",
                table: "UserContact",
                column: "PhoneNumber",
                unique: true,
                filter: "[PhoneNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserContact_SSN",
                table: "UserContact",
                column: "SSN",
                unique: true,
                filter: "[SSN] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserContact_TaxIdNumber",
                table: "UserContact",
                column: "TaxIdNumber",
                unique: true,
                filter: "[TaxIdNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Name",
                table: "Permissions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_Domain",
                table: "Organizations",
                column: "Domain",
                unique: true,
                filter: "[Domain] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_UserEmploymentInformations_Department_DepartmentId",
                table: "UserEmploymentInformations",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserEmploymentInformations_Department_DepartmentId",
                table: "UserEmploymentInformations");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_UserFinancials_AccountNumber",
                table: "UserFinancials");

            migrationBuilder.DropIndex(
                name: "IX_UserEmploymentInformations_DepartmentId",
                table: "UserEmploymentInformations");

            migrationBuilder.DropIndex(
                name: "IX_UserEmploymentInformations_EmployeeID",
                table: "UserEmploymentInformations");

            migrationBuilder.DropIndex(
                name: "IX_UserContact_MobileNumber",
                table: "UserContact");

            migrationBuilder.DropIndex(
                name: "IX_UserContact_NationalId",
                table: "UserContact");

            migrationBuilder.DropIndex(
                name: "IX_UserContact_PassportNumber",
                table: "UserContact");

            migrationBuilder.DropIndex(
                name: "IX_UserContact_PersonalEmail",
                table: "UserContact");

            migrationBuilder.DropIndex(
                name: "IX_UserContact_PhoneNumber",
                table: "UserContact");

            migrationBuilder.DropIndex(
                name: "IX_UserContact_SSN",
                table: "UserContact");

            migrationBuilder.DropIndex(
                name: "IX_UserContact_TaxIdNumber",
                table: "UserContact");

            migrationBuilder.DropIndex(
                name: "IX_Roles_Name",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_Name",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Organizations_Domain",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "UserEmploymentInformations");

            migrationBuilder.DropColumn(
                name: "EmployeeID",
                table: "UserEmploymentInformations");

            migrationBuilder.DropColumn(
                name: "EmploymentContract",
                table: "UserEmploymentInformations");

            migrationBuilder.DropColumn(
                name: "EmploymentType",
                table: "UserEmploymentInformations");

            migrationBuilder.DropColumn(
                name: "JobTtle",
                table: "UserEmploymentInformations");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Roles",
                newName: "RoleName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Permissions",
                newName: "PermissionName");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Contract",
                table: "UserEmploymentInformations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
