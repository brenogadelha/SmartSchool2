using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSchool.Dominio.Tccs;

namespace SmartSchool.Dados.Modulos.Professores
{
	public class TccProfessorMapeamento : IEntityTypeConfiguration<TccProfessor>
	{
		public void Configure(EntityTypeBuilder<TccProfessor> builder)
		{
			builder.ToTable("TCC_PROFESSOR");

			builder.HasKey(dp => new { dp.ProfessorID, dp.TccID });

			builder.Property(dp => dp.ProfessorID)
				   .HasColumnName("TCPR_ID_PROFESSOR")
				   .IsRequired();

			builder.Property(dp => dp.TccID)
				   .HasColumnName("TCPR_ID_TCC")
				   .IsRequired();

			builder.HasOne(dp => dp.Professor)
				   .WithMany(b => b.Tccs)
				   .HasForeignKey(dp => dp.ProfessorID)
				   .HasConstraintName("FK_PROF_TCC")
				   .OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(dp => dp.Tcc)
				   .WithMany(p => p.TccProfessores)
				   .HasForeignKey(dp => dp.TccID)
				   .HasConstraintName("FK_TCC_PROF")
				   .OnDelete(DeleteBehavior.Cascade);
		}
	}
}
