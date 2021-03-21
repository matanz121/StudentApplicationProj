using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentsApplicationProj.Server.Migrations
{
    public partial class FixRelationAndAddApplicationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationDateTime",
                table: "StudentCourse");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "StudentCourse");

            migrationBuilder.AddColumn<double>(
                name: "Grade",
                table: "Course",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "CourseApplication",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicationBody = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ApplicationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudentCourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseApplication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseApplication_StudentCourse_StudentCourseId",
                        column: x => x.StudentCourseId,
                        principalTable: "StudentCourse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileUrl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CourseApplicationId = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileUrl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileUrl_CourseApplication_CourseApplicationId",
                        column: x => x.CourseApplicationId,
                        principalTable: "CourseApplication",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseApplication_StudentCourseId",
                table: "CourseApplication",
                column: "StudentCourseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileUrl_CourseApplicationId",
                table: "FileUrl",
                column: "CourseApplicationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileUrl");

            migrationBuilder.DropTable(
                name: "CourseApplication");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "Course");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApplicationDateTime",
                table: "StudentCourse",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "StudentCourse",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
