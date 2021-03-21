using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentsApplicationProj.Server.Migrations
{
    public partial class FixDepartmentHeadDuplicateKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_SystemUser_DepartmentHeadId1",
                table: "Department");

            migrationBuilder.DropIndex(
                name: "IX_Department_DepartmentHeadId1",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "DepartmentHeadId",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "DepartmentHeadId1",
                table: "Department");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId1",
                table: "SystemUser",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SystemUser_DepartmentId1",
                table: "SystemUser",
                column: "DepartmentId1",
                unique: true,
                filter: "[DepartmentId1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemUser_Department_DepartmentId1",
                table: "SystemUser",
                column: "DepartmentId1",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SystemUser_Department_DepartmentId1",
                table: "SystemUser");

            migrationBuilder.DropIndex(
                name: "IX_SystemUser_DepartmentId1",
                table: "SystemUser");

            migrationBuilder.DropColumn(
                name: "DepartmentId1",
                table: "SystemUser");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentHeadId",
                table: "Department",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentHeadId1",
                table: "Department",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Department_DepartmentHeadId1",
                table: "Department",
                column: "DepartmentHeadId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Department_SystemUser_DepartmentHeadId1",
                table: "Department",
                column: "DepartmentHeadId1",
                principalTable: "SystemUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
