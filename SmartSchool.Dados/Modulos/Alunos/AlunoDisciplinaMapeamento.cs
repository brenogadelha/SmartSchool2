using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSchool.Dominio.Alunos;

namespace SmartSchool.Dados.Modulos.Alunos
{
	public class AlunoDisciplinaMapeamento : IEntityTypeConfiguration<AlunoDisciplina>
	{
		public void Configure(EntityTypeBuilder<AlunoDisciplina> builder)
		{
			builder.ToTable("ALUNO_DISCIPLINA");

			builder.HasKey(dp => new { dp.AlunoID, dp.DisciplinaID });

			builder.Property(dp => dp.AlunoID)
				   .HasColumnName("ALDI_ID_ALUNO");

			builder.Property(dp => dp.DisciplinaID)
				   .HasColumnName("ALDI_ID_DISCIPLINA");

			builder.HasOne(dp => dp.Aluno)
				   .WithMany(b => b.AlunosDisciplinas)
				   .HasForeignKey(dp => dp.AlunoID)
				   .HasConstraintName("FK_ALUN_DISC")
				   .OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(dp => dp.Disciplina)
				   .WithMany(p => p.Alunos)
				   .HasForeignKey(dp => dp.DisciplinaID)
				   .HasConstraintName("FK_DISC_ALUN")
				   .OnDelete(DeleteBehavior.Cascade);
		}
	}
}
