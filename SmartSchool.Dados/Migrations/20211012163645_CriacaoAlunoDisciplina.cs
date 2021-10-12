using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSchool.Dados.Migrations
{
    public partial class CriacaoAlunoDisciplina : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ALUNO_DISCIPLINA",
                schema: "SmartSchool",
                columns: table => new
                {
                    ALDI_ID_ALUNO = table.Column<Guid>(nullable: false),
                    ALDI_ID_DISCIPLINA = table.Column<Guid>(nullable: false),
                    ALDI_ID_STATUS_DISCIPLINA = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ALUNO_DISCIPLINA", x => new { x.ALDI_ID_ALUNO, x.ALDI_ID_DISCIPLINA, x.ALDI_ID_STATUS_DISCIPLINA });
                    table.ForeignKey(
                        name: "FK_ALUN_DISC",
                        column: x => x.ALDI_ID_ALUNO,
                        principalSchema: "SmartSchool",
                        principalTable: "ALUNO",
                        principalColumn: "ALUN_ID_ALUNO",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DISC_ALUN",
                        column: x => x.ALDI_ID_DISCIPLINA,
                        principalSchema: "SmartSchool",
                        principalTable: "DISCIPLINA",
                        principalColumn: "DISC_ID_DISCIPLINA",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ALUNO_DISCIPLINA_ALDI_ID_DISCIPLINA",
                schema: "SmartSchool",
                table: "ALUNO_DISCIPLINA",
                column: "ALDI_ID_DISCIPLINA");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ALUNO_DISCIPLINA",
                schema: "SmartSchool");
        }
    }
}
