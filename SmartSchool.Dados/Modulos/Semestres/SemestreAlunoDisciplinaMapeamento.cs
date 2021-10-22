using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSchool.Dominio.Semestres;

namespace SmartSchool.Dados.Modulos.Semestres
{
	public class SemestreAlunoDisciplinaMapeamento : IEntityTypeConfiguration<SemestreAlunoDisciplina>
	{
		public void Configure(EntityTypeBuilder<SemestreAlunoDisciplina> builder)
		{
			builder.ToTable("SEMESTRE_ALUNO_DISCIPLINA");

			builder.HasKey(dp => new { dp.SemestreID, dp.AlunoID, dp.DisciplinaID });

			builder.Property(dp => dp.SemestreID)
				   .HasColumnName("SEAD_ID_SEMESTRE");

			builder.Property(dp => dp.AlunoID)
				   .HasColumnName("SEAD_ID_ALUNO");

			builder.Property(dp => dp.DisciplinaID)
				   .HasColumnName("SEAD_ID_DISCIPLINA");

			builder.Property(dp => dp.Periodo)
				   .HasColumnName("SEAD_ID_PERIODO")
				   .IsRequired();

			builder.Property(dp => (int)dp.StatusDisciplina)
				   .HasColumnName("SEAD_ID_STATUS_DISCIPLINA")
				   .IsRequired();

			builder.HasOne(dp => dp.Semestre)
				   .WithMany(b => b.AlunosDisciplinas)
				   .HasForeignKey(dp => dp.SemestreID)
				   .HasConstraintName("FK_SEME_ALDI")
				   .OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(dp => dp.AlunoDisciplina)
				   .WithMany(p => p.Semestres)
				   .HasForeignKey(dp => new { dp.AlunoID, dp.DisciplinaID })
				   .HasConstraintName("FK_ALDI_SEME")
				   .OnDelete(DeleteBehavior.Restrict);
		}
	}
}
