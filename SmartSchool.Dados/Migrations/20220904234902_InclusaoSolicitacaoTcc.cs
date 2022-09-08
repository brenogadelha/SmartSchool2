using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSchool.Dados.Migrations
{
    public partial class InclusaoSolicitacaoTcc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TAPR_TXT_RESPOSTA_SOLICITACAO",
                schema: "SmartSchool",
                table: "TCC_ALUNO_PROFESSOR",
                type: "nvarchar(1008)",
                maxLength: 1008,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TAPR_TXT_SOLICITACAO",
                schema: "SmartSchool",
                table: "TCC_ALUNO_PROFESSOR",
                type: "nvarchar(1008)",
                maxLength: 1008,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TAPR_TXT_RESPOSTA_SOLICITACAO",
                schema: "SmartSchool",
                table: "TCC_ALUNO_PROFESSOR");

            migrationBuilder.DropColumn(
                name: "TAPR_TXT_SOLICITACAO",
                schema: "SmartSchool",
                table: "TCC_ALUNO_PROFESSOR");
        }
    }
}
