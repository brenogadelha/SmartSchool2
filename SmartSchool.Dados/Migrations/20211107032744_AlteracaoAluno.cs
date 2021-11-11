using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSchool.Dados.Migrations
{
    public partial class AlteracaoAluno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ALUN_NR_CELULAR",
                schema: "SmartSchool",
                table: "ALUNO",
                maxLength: 16,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ALUN_TXT_CIDADE",
                schema: "SmartSchool",
                table: "ALUNO",
                maxLength: 32,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ALUN_NR_CPF",
                schema: "SmartSchool",
                table: "ALUNO",
                maxLength: 16,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ALUN_TXT_EMAIL",
                schema: "SmartSchool",
                table: "ALUNO",
                maxLength: 256,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ALUN_NR_CELULAR",
                schema: "SmartSchool",
                table: "ALUNO");

            migrationBuilder.DropColumn(
                name: "ALUN_TXT_CIDADE",
                schema: "SmartSchool",
                table: "ALUNO");

            migrationBuilder.DropColumn(
                name: "ALUN_NR_CPF",
                schema: "SmartSchool",
                table: "ALUNO");

            migrationBuilder.DropColumn(
                name: "ALUN_TXT_EMAIL",
                schema: "SmartSchool",
                table: "ALUNO");
        }
    }
}
