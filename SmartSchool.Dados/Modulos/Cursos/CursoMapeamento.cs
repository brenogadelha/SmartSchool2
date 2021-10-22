using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSchool.Dominio.Alunos;
using SmartSchool.Dominio.Cursos;
using SmartSchool.Dominio.Disciplinas;

namespace SmartSchool.Dados.Modulos.Usuarios
{
	public class CursoMapeamento : IEntityTypeConfiguration<Curso>
	{
		public void Configure(EntityTypeBuilder<Curso> builder)
		{
			builder.ToTable("CURSO");

			builder.Property(b => b.ID)
					.HasColumnName("CURS_ID_CURSO")
					.IsRequired();

			builder.Property(b => b.Nome)
					.HasColumnName("CURS_NM_CURSO")
					.HasMaxLength(32)
					.IsRequired();
		}
	}
}
