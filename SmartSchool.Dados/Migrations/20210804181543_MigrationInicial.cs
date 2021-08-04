using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSchool.Dados.Migrations
{
    public partial class MigrationInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "SmartSchool");

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
                    ALUN_IN_ATIVO = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ALUNO", x => x.ALUN_ID_ALUNO);
                });

            migrationBuilder.CreateTable(
                name: "DISCIPLINA",
                schema: "SmartSchool",
                columns: table => new
                {
                    DISC_ID_DISCIPLINA = table.Column<Guid>(nullable: false),
                    DISC_NM_NOME = table.Column<string>(maxLength: 32, nullable: false)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ALUNO",
                schema: "SmartSchool");

            migrationBuilder.DropTable(
                name: "DISCIPLINA",
                schema: "SmartSchool");

            migrationBuilder.DropTable(
                name: "PROFESSOR",
                schema: "SmartSchool");
        }
    }
}
