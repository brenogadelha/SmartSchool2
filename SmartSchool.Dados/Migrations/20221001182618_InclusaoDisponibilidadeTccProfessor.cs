using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSchool.Dados.Migrations
{
    public partial class InclusaoDisponibilidadeTccProfessor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PROF_ID_DISPONIBILIDADE_TCC",
                schema: "SmartSchool",
                table: "PROFESSOR",
                type: "int",
                nullable: false,
                defaultValue: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PROF_ID_DISPONIBILIDADE_TCC",
                schema: "SmartSchool",
                table: "PROFESSOR");
        }
    }
}
