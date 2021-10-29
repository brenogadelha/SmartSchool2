using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSchool.Dominio.Professores;

namespace SmartSchool.Dados.Modulos.Professores
{
	public class ProfessorDisciplinaMapeamento : IEntityTypeConfiguration<ProfessorDisciplina>
	{
		public void Configure(EntityTypeBuilder<ProfessorDisciplina> builder)
		{
			builder.ToTable("PROFESSOR_DISCIPLINA");

			builder.HasKey(dp => new { dp.ProfessorID, dp.DisciplinaID });

			builder.Property(dp => dp.ProfessorID)
				   .HasColumnName("PRDI_ID_PROFESSOR");

			builder.Property(dp => dp.DisciplinaID)
				   .HasColumnName("PRDI_ID_DISCIPLINA");

			builder.HasOne(dp => dp.Professor)
				   .WithMany(b => b.ProfessoresDisciplinas)
				   .HasForeignKey(dp => dp.ProfessorID)
				   .HasConstraintName("FK_PROF_DISC")
				   .OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(dp => dp.Disciplina)
				   .WithMany(p => p.Professores)
				   .HasForeignKey(dp => dp.DisciplinaID)
				   .HasConstraintName("FK_DISC_PROF")
				   .OnDelete(DeleteBehavior.Cascade);
		}
	}
}
