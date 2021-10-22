using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSchool.Dados.Migrations
{
	public partial class CriacaoMigrationInicial : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.EnsureSchema(
				name: "SmartSchool");

			migrationBuilder.CreateTable(
				name: "CURSO",
				schema: "SmartSchool",
				columns: table => new
				{
					CURS_ID_CURSO = table.Column<Guid>(nullable: false),
					CURS_NM_CURSO = table.Column<string>(maxLength: 32, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_CURSO", x => x.CURS_ID_CURSO);
				});

			migrationBuilder.CreateTable(
				name: "DISCIPLINA",
				schema: "SmartSchool",
				columns: table => new
				{
					DISC_ID_DISCIPLINA = table.Column<Guid>(nullable: false),
					DISC_NM_NOME = table.Column<string>(maxLength: 32, nullable: false),
					DISC_ID_PERIODO = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_DISCIPLINA", x => x.DISC_ID_DISCIPLINA);
				});

			migrationBuilder.CreateTable(
				name: "PROFESSOR",
				schema: "SmartSchool",
				columns: table => new
				{
					PROF_ID_PROFESSOR = table.Column<Guid>(nullable: false),
					PROF_COD_PROFESSOR = table.Column<int>(nullable: false),
					PROF_NM_NOME = table.Column<string>(maxLength: 32, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_PROFESSOR", x => x.PROF_ID_PROFESSOR);
				});

			migrationBuilder.CreateTable(
				name: "SEMESTRE",
				schema: "SmartSchool",
				columns: table => new
				{
					SEME_ID_SEMESTRE = table.Column<Guid>(nullable: false),
					SEME_DT_INICIO = table.Column<DateTime>(nullable: false),
					SEME_DT_FIM = table.Column<DateTime>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_SEMESTRE", x => x.SEME_ID_SEMESTRE);
				});

			migrationBuilder.CreateTable(
				name: "ALUNO",
				schema: "SmartSchool",
				columns: table => new
				{
					ALUN_ID_ALUNO = table.Column<Guid>(nullable: false),
					ALUN_COD_ALUNO = table.Column<int>(nullable: false),
					ALUN_NM_NOME = table.Column<string>(maxLength: 32, nullable: false),
					ALUN_NM_SOBRENOME = table.Column<string>(maxLength: 128, nullable: false),
					ALUN_NR_TELEFONE = table.Column<string>(maxLength: 16, nullable: true),
					ALUN_DT_NASCIMENTO = table.Column<DateTime>(nullable: false),
					ALUN_DT_INICIO = table.Column<DateTime>(nullable: false),
					ALUN_DT_FIM = table.Column<DateTime>(nullable: false),
					ALUN_IN_ATIVO = table.Column<bool>(nullable: false),
					ALUN_ID_CURSO = table.Column<Guid>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ALUNO", x => x.ALUN_ID_ALUNO);
					table.ForeignKey(
						name: "FK_ALUN_CURSO",
						column: x => x.ALUN_ID_CURSO,
						principalSchema: "SmartSchool",
						principalTable: "CURSO",
						principalColumn: "CURS_ID_CURSO",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "CURSO_DISCIPLINA",
				schema: "SmartSchool",
				columns: table => new
				{
					CUDI_ID_CURSO = table.Column<Guid>(nullable: false),
					CUDI_ID_DISCIPLINA = table.Column<Guid>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_CURSO_DISCIPLINA", x => new { x.CUDI_ID_CURSO, x.CUDI_ID_DISCIPLINA });
					table.ForeignKey(
						name: "FK_CURS_DISC",
						column: x => x.CUDI_ID_CURSO,
						principalSchema: "SmartSchool",
						principalTable: "CURSO",
						principalColumn: "CURS_ID_CURSO",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_DISC_CURS",
						column: x => x.CUDI_ID_DISCIPLINA,
						principalSchema: "SmartSchool",
						principalTable: "DISCIPLINA",
						principalColumn: "DISC_ID_DISCIPLINA",
						onDelete: ReferentialAction.Cascade);
				});

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

			migrationBuilder.CreateTable(
				name: "ALUNO_DISCIPLINA",
				schema: "SmartSchool",
				columns: table => new
				{
					ALDI_ID_ALUNO = table.Column<Guid>(nullable: false),
					ALDI_ID_DISCIPLINA = table.Column<Guid>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ALUNO_DISCIPLINA", x => new { x.ALDI_ID_ALUNO, x.ALDI_ID_DISCIPLINA });
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

			migrationBuilder.CreateTable(
				name: "SEMESTRE_ALUNO_DISCIPLINA",
				schema: "SmartSchool",
				columns: table => new
				{
					SEAD_ID_SEMESTRE = table.Column<Guid>(nullable: false),
					SEAD_ID_DISCIPLINA = table.Column<Guid>(nullable: false),
					SEAD_ID_ALUNO = table.Column<Guid>(nullable: false),
					SEAD_ID_PERIODO = table.Column<int>(nullable: false),
					SEAD_ID_STATUS_DISCIPLINA = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_SEMESTRE_ALUNO_DISCIPLINA", x => new { x.SEAD_ID_SEMESTRE, x.SEAD_ID_ALUNO, x.SEAD_ID_DISCIPLINA });
					table.ForeignKey(
						name: "FK_SEMESTRE_ALUNO_DISCIPLINA_ALUNO_SEAD_ID_ALUNO",
						column: x => x.SEAD_ID_ALUNO,
						principalSchema: "SmartSchool",
						principalTable: "ALUNO",
						principalColumn: "ALUN_ID_ALUNO",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_SEME_ALDI",
						column: x => x.SEAD_ID_SEMESTRE,
						principalSchema: "SmartSchool",
						principalTable: "SEMESTRE",
						principalColumn: "SEME_ID_SEMESTRE",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_ALDI_SEME",
						columns: x => new { x.SEAD_ID_ALUNO, x.SEAD_ID_DISCIPLINA },
						principalSchema: "SmartSchool",
						principalTable: "ALUNO_DISCIPLINA",
						principalColumns: new[] { "ALDI_ID_ALUNO", "ALDI_ID_DISCIPLINA" },
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateIndex(
				name: "IX_ALUNO_ALUN_ID_CURSO",
				schema: "SmartSchool",
				table: "ALUNO",
				column: "ALUN_ID_CURSO");

			migrationBuilder.CreateIndex(
				name: "IX_ALUNO_DISCIPLINA_ALDI_ID_DISCIPLINA",
				schema: "SmartSchool",
				table: "ALUNO_DISCIPLINA",
				column: "ALDI_ID_DISCIPLINA");

			migrationBuilder.CreateIndex(
				name: "IX_CURSO_DISCIPLINA_CUDI_ID_DISCIPLINA",
				schema: "SmartSchool",
				table: "CURSO_DISCIPLINA",
				column: "CUDI_ID_DISCIPLINA");

			migrationBuilder.CreateIndex(
				name: "IX_PROFESSOR_DISCIPLINA_PRDI_ID_DISCIPLINA",
				schema: "SmartSchool",
				table: "PROFESSOR_DISCIPLINA",
				column: "PRDI_ID_DISCIPLINA");

			migrationBuilder.CreateIndex(
				name: "IX_SEMESTRE_ALUNO_DISCIPLINA_SEAD_ID_ALUNO_SEAD_ID_DISCIPLINA",
				schema: "SmartSchool",
				table: "SEMESTRE_ALUNO_DISCIPLINA",
				columns: new[] { "SEAD_ID_ALUNO", "SEAD_ID_DISCIPLINA" });
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "CURSO_DISCIPLINA",
				schema: "SmartSchool");

			migrationBuilder.DropTable(
				name: "PROFESSOR_DISCIPLINA",
				schema: "SmartSchool");

			migrationBuilder.DropTable(
				name: "SEMESTRE_ALUNO_DISCIPLINA",
				schema: "SmartSchool");

			migrationBuilder.DropTable(
				name: "PROFESSOR",
				schema: "SmartSchool");

			migrationBuilder.DropTable(
				name: "SEMESTRE",
				schema: "SmartSchool");

			migrationBuilder.DropTable(
				name: "ALUNO_DISCIPLINA",
				schema: "SmartSchool");

			migrationBuilder.DropTable(
				name: "ALUNO",
				schema: "SmartSchool");

			migrationBuilder.DropTable(
				name: "DISCIPLINA",
				schema: "SmartSchool");

			migrationBuilder.DropTable(
				name: "CURSO",
				schema: "SmartSchool");
		}
	}
}
