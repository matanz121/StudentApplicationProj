using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentsApplicationProj.Server.Migrations
{
    public partial class updateFileUploadModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FileUrl_CourseApplicationId",
                table: "FileUrl");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "FileUrl");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "FileUrl");

            migrationBuilder.AddColumn<string>(
                name: "CertificatePath",
                table: "FileUrl",
                type: "nvarchar(256)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GradeSheetPath",
                table: "FileUrl",
                type: "nvarchar(256)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SyllabusPath",
                table: "FileUrl",
                type: "nvarchar(256)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileUrl_CourseApplicationId",
                table: "FileUrl",
                column: "CourseApplicationId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FileUrl_CourseApplicationId",
                table: "FileUrl");

            migrationBuilder.DropColumn(
                name: "CertificatePath",
                table: "FileUrl");

            migrationBuilder.DropColumn(
                name: "GradeSheetPath",
                table: "FileUrl");

            migrationBuilder.DropColumn(
                name: "SyllabusPath",
                table: "FileUrl");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "FileUrl",
                type: "nvarchar(128)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "FileUrl",
                type: "nvarchar(258)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_FileUrl_CourseApplicationId",
                table: "FileUrl",
                column: "CourseApplicationId");
        }
    }
}
