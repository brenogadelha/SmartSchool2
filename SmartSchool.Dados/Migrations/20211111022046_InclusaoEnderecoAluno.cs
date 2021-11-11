using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSchool.Dados.Migrations
{
    public partial class InclusaoEnderecoAluno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ALUN_TXT_ENDERECO",
                schema: "SmartSchool",
                table: "ALUNO",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ALUN_TXT_ENDERECO",
                schema: "SmartSchool",
                table: "ALUNO");
        }
    }
}
