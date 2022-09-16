using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSchool.Dados.Migrations
{
    public partial class AjusteTcc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TCC_TXT_OBJETIVO",
                schema: "SmartSchool",
                table: "TCC");

            migrationBuilder.DropColumn(
                name: "TCC_TXT_PROBLEMATICA",
                schema: "SmartSchool",
                table: "TCC");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TCC_TXT_OBJETIVO",
                schema: "SmartSchool",
                table: "TCC",
                type: "nvarchar(502)",
                maxLength: 502,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TCC_TXT_PROBLEMATICA",
                schema: "SmartSchool",
                table: "TCC",
                type: "nvarchar(3008)",
                maxLength: 3008,
                nullable: false,
                defaultValue: "");
        }
    }
}
