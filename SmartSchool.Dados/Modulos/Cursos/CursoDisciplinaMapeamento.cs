using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSchool.Dominio.Disciplinas;

namespace SmartSchool.Dados.Modulos.Alunos
{
	public class CursoDisciplinaMapeamento : IEntityTypeConfiguration<CursoDisciplina>
	{
		public void Configure(EntityTypeBuilder<CursoDisciplina> builder)
		{
			builder.ToTable("CURSO_DISCIPLINA");

			builder.HasKey(dp => new { dp.CursoID, dp.DisciplinaID });

			builder.Property(dp => dp.CursoID)
				   .HasColumnName("CUDI_ID_CURSO");

			builder.Property(dp => dp.DisciplinaID)
				   .HasColumnName("CUDI_ID_DISCIPLINA");

			builder.HasOne(dp => dp.Curso)
				   .WithMany(b => b.CursosDisciplinas)
				   .HasForeignKey(dp => dp.CursoID)
				   .HasConstraintName("FK_CURS_DISC")
				   .OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(dp => dp.Disciplina)
				   .WithMany(p => p.Cursos)
				   .HasForeignKey(dp => dp.DisciplinaID)
				   .HasConstraintName("FK_DISC_CURS")
				   .OnDelete(DeleteBehavior.Cascade);
		}
	}
}
