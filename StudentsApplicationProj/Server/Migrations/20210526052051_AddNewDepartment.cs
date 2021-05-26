using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentsApplicationProj.Server.Migrations
{
    public partial class AddNewDepartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 1,
                column: "DepartmentName",
                value: "CSE");

            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "Id", "DepartmentHeadId", "DepartmentName" },
                values: new object[] { 3, null, "ME" });

            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "Id", "DepartmentHeadId", "DepartmentName" },
                values: new object[] { 2, null, "EEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Department",
                keyColumn: "Id",
                keyValue: 1,
                column: "DepartmentName",
                value: "Computer Science and Engineering");
        }
    }
}
