using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSchool.Dominio.Tccs;

namespace SmartSchool.Dados.Modulos.Tccs
{
	public class TccAlunoProfessorMapeamento : IEntityTypeConfiguration<TccAlunoProfessor>
	{
		public void Configure(EntityTypeBuilder<TccAlunoProfessor> builder)
		{
			builder.ToTable("TCC_ALUNO_PROFESSOR");

			builder.HasKey(dp => new { dp.TccID, dp.AlunoID, dp.ProfessorID });

			builder.Property(dp => dp.TccID)
				   .HasColumnName("TAPR_ID_TCC");

			builder.Property(dp => dp.AlunoID)
				   .HasColumnName("TAPR_ID_ALUNO");

			builder.Property(dp => dp.ProfessorID)
				   .HasColumnName("TAPR_ID_PROFESSOR");

			builder.Property(dp => dp.DataSolicitacao)
				   .HasColumnName("TAPR_ID_DATA_SOLICITACAO")
				   .IsRequired();

			builder.Property(dp => (int)dp.Status)
				   .HasColumnName("TAPR_ID_STATUS_TCC")
				   .IsRequired();

			builder.Property(dp => dp.Solicitacao)
				   .HasColumnName("TAPR_TXT_SOLICITACAO")
				   .HasMaxLength(1008);

			builder.Property(dp => dp.RespostaSolicitacao)
				   .HasColumnName("TAPR_TXT_RESPOSTA_SOLICITACAO")
				   .HasMaxLength(1008);

			builder.HasOne(dp => dp.Aluno)
				   .WithMany(b => b.TccsProfessores)
				   .HasForeignKey(dp => dp.AlunoID)
				   .HasConstraintName("FK_ALUN_TAPR")
				   .OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(dp => dp.ProfessorTcc)
				   .WithMany(p => p.Alunos)
				   .HasForeignKey(dp => new { dp.ProfessorID, dp.TccID })
				   .HasConstraintName("FK_TAPR_ALUN")
				   .OnDelete(DeleteBehavior.Restrict);
		}
	}
}
