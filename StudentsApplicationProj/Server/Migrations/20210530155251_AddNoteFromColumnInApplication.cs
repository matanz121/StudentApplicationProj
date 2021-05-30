using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentsApplicationProj.Server.Migrations
{
    public partial class AddNoteFromColumnInApplication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ApplicationDateTime",
                table: "CourseApplication",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AddColumn<string>(
                name: "NoteFrom",
                table: "CourseApplication",
                type: "nvarchar(256)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoteFrom",
                table: "CourseApplication");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApplicationDateTime",
                table: "CourseApplication",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
