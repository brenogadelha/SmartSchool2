using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSchool.Dados.Migrations
{
    public partial class CriacaoProfessorDisciplina : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PROFESSOR_DISCIPLINA",
                schema: "SmartSchool",
                columns: table => new
                {
                    PRDI_ID_PROFESSOR = table.Column<Guid>(nullable: false),
                    PRDI_ID_DISCIPLINA = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROFESSOR_DISCIPLINA", x => new { x.PRDI_ID_PROFESSOR, x.PRDI_ID_DISCIPLINA });
                    table.ForeignKey(
                        name: "FK_DISC_PROF",
                        column: x => x.PRDI_ID_DISCIPLINA,
                        principalSchema: "SmartSchool",
                        principalTable: "DISCIPLINA",
                        principalColumn: "DISC_ID_DISCIPLINA",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PROF_DISC",
                        column: x => x.PRDI_ID_PROFESSOR,
                        principalSchema: "SmartSchool",
                        principalTable: "PROFESSOR",
                        principalColumn: "PROF_ID_PROFESSOR",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PROFESSOR_DISCIPLINA_PRDI_ID_DISCIPLINA",
                schema: "SmartSchool",
                table: "PROFESSOR_DISCIPLINA",
                column: "PRDI_ID_DISCIPLINA");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PROFESSOR_DISCIPLINA",
                schema: "SmartSchool");
        }
    }
}
