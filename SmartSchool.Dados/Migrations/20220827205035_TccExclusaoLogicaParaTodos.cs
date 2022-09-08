using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSchool.Dados.Migrations
{
    public partial class TccExclusaoLogicaParaTodos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SEME_IN_ATIVO",
                schema: "SmartSchool",
                table: "SEMESTRE",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PROF_IN_ATIVO",
                schema: "SmartSchool",
                table: "PROFESSOR",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DISC_IN_ATIVO",
                schema: "SmartSchool",
                table: "DISCIPLINA",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CURS_IN_ATIVO",
                schema: "SmartSchool",
                table: "CURSO",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "TCC",
                schema: "SmartSchool",
                columns: table => new
                {
                    TCC_ID_TCC = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TCC_NM_TEMA = table.Column<string>(type: "nvarchar(160)", maxLength: 160, nullable: false),
                    TCC_TXT_DESCRIÇÃO = table.Column<string>(type: "nvarchar(3008)", maxLength: 3008, nullable: true),
                    TCC_TXT_OBJETIVO = table.Column<string>(type: "nvarchar(502)", maxLength: 502, nullable: false),
                    TCC_TXT_PROBLEMATICA = table.Column<string>(type: "nvarchar(3008)", maxLength: 3008, nullable: false),
                    TCC_IN_ATIVO = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TCC", x => x.TCC_ID_TCC);
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
                        principalTable: "TCC",
                        principalColumn: "TCC_ID_TCC",
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
                name: "TCC",
                schema: "SmartSchool");

            migrationBuilder.DropColumn(
                name: "SEME_IN_ATIVO",
                schema: "SmartSchool",
                table: "SEMESTRE");

            migrationBuilder.DropColumn(
                name: "PROF_IN_ATIVO",
                schema: "SmartSchool",
                table: "PROFESSOR");

            migrationBuilder.DropColumn(
                name: "DISC_IN_ATIVO",
                schema: "SmartSchool",
                table: "DISCIPLINA");

            migrationBuilder.DropColumn(
                name: "CURS_IN_ATIVO",
                schema: "SmartSchool",
                table: "CURSO");
        }
    }
}
