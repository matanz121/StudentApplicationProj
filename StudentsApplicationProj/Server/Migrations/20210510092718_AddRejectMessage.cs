using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentsApplicationProj.Server.Migrations
{
    public partial class AddRejectMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RejectMessage",
                table: "CourseApplication",
                type: "nvarchar(MAX)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RejectMessage",
                table: "CourseApplication");
        }
    }
}
