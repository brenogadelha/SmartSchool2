using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSchool.Dados.Migrations
{
    public partial class CriacaoTcc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tccs",
                schema: "SmartSchool",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Tema = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Objetivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Problematica = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tccs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TCC_PROFESSOR",
                schema: "SmartSchool",
                columns: table => new
                {
                    TCPR_ID_PROFESSOR = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TCPR_ID_TCC = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TCC_PROFESSOR", x => new { x.TCPR_ID_PROFESSOR, x.TCPR_ID_TCC });
                    table.ForeignKey(
                        name: "FK_PROF_TCC",
                        column: x => x.TCPR_ID_PROFESSOR,
                        principalSchema: "SmartSchool",
                        principalTable: "PROFESSOR",
                        principalColumn: "PROF_ID_PROFESSOR",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TCC_PROF",
                        column: x => x.TCPR_ID_TCC,
                        principalSchema: "SmartSchool",
                        principalTable: "Tccs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TCC_ALUNO_PROFESSOR",
                schema: "SmartSchool",
                columns: table => new
                {
                    TAPR_ID_TCC = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TAPR_ID_PROFESSOR = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TAPR_ID_ALUNO = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TAPR_ID_DATA_SOLICITACAO = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TAPR_ID_STATUS_TCC = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TCC_ALUNO_PROFESSOR", x => new { x.TAPR_ID_TCC, x.TAPR_ID_ALUNO, x.TAPR_ID_PROFESSOR });
                    table.ForeignKey(
                        name: "FK_ALUN_TAPR",
                        column: x => x.TAPR_ID_ALUNO,
                        principalSchema: "SmartSchool",
                        principalTable: "ALUNO",
                        principalColumn: "ALUN_ID_ALUNO",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TAPR_ALUN",
                        columns: x => new { x.TAPR_ID_ALUNO, x.TAPR_ID_PROFESSOR },
                        principalSchema: "SmartSchool",
                        principalTable: "TCC_PROFESSOR",
                        principalColumns: new[] { "TCPR_ID_PROFESSOR", "TCPR_ID_TCC" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TCC_ALUNO_PROFESSOR_TAPR_ID_ALUNO_TAPR_ID_PROFESSOR",
                schema: "SmartSchool",
                table: "TCC_ALUNO_PROFESSOR",
                columns: new[] { "TAPR_ID_ALUNO", "TAPR_ID_PROFESSOR" });

            migrationBuilder.CreateIndex(
                name: "IX_TCC_PROFESSOR_TCPR_ID_TCC",
                schema: "SmartSchool",
                table: "TCC_PROFESSOR",
                column: "TCPR_ID_TCC");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TCC_ALUNO_PROFESSOR",
                schema: "SmartSchool");

            migrationBuilder.DropTable(
                name: "TCC_PROFESSOR",
                schema: "SmartSchool");

            migrationBuilder.DropTable(
                name: "Tccs",
                schema: "SmartSchool");
        }
    }
}
