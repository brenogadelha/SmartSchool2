using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSchool.Comum.Dominio.Enums;
using SmartSchool.Dominio.Professores;

namespace SmartSchool.Dados.Modulos.Usuarios
{
	public class ProfessorMapeamento : IEntityTypeConfiguration<Professor>
	{
		public void Configure(EntityTypeBuilder<Professor> builder)
		{
			builder.ToTable("PROFESSOR");

			builder.Property(b => b.ID)
					.HasColumnName("PROF_ID_PROFESSOR")
					.ValueGeneratedNever()
					.IsRequired();

			builder.Property(b => b.Nome)
					.HasColumnName("PROF_NM_NOME")
					.HasMaxLength(160)
					.IsRequired();

			builder.Property(b => b.Matricula)
					.HasColumnName("PROF_COD_PROFESSOR")
					.ValueGeneratedNever()
					.IsRequired();

			builder.Property(b => b.Ativo)
				   .HasColumnName("PROF_IN_ATIVO")
				   .IsRequired();

			builder.Property(dp => (int)dp.DisponibilidadeTcc)
				   .HasColumnName("PROF_ID_DISPONIBILIDADE_TCC")
				   .HasDefaultValue(DisponibilidadeTcc.Indisponível)
				   .IsRequired();
		}
	}
}
