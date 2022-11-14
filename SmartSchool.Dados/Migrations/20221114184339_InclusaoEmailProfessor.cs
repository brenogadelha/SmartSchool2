using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSchool.Dados.Migrations
{
    public partial class InclusaoEmailProfessor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PROF_TXT_EMAIL",
                schema: "SmartSchool",
                table: "PROFESSOR",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PROF_TXT_EMAIL",
                schema: "SmartSchool",
                table: "PROFESSOR");
        }
    }
}
