using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSchool.Dominio.Tccs;

namespace SmartSchool.Dados.Modulos.Tccs
{
	public class TccMapeamento : IEntityTypeConfiguration<Tcc>
	{
		public void Configure(EntityTypeBuilder<Tcc> builder)
		{
			builder.ToTable("TCC");

			builder.Property(b => b.ID)
					.HasColumnName("TCC_ID_TCC")
					.ValueGeneratedNever()
					.IsRequired();

			builder.Property(b => b.Tema)
					.HasColumnName("TCC_NM_TEMA")
					.HasMaxLength(160)
					.IsRequired();

			builder.Property(b => b.Objetivo)
					.HasColumnName("TCC_TXT_OBJETIVO")
					.HasMaxLength(502)
					.IsRequired();

			builder.Property(b => b.Descricao)
					.HasColumnName("TCC_TXT_DESCRIÇÃO")
					.HasMaxLength(3008);

			builder.Property(b => b.Problematica)
					.HasColumnName("TCC_TXT_PROBLEMATICA")
					.HasMaxLength(3008)
					.IsRequired();

			builder.Property(b => b.Ativo)
				   .HasColumnName("TCC_IN_ATIVO")
				   .IsRequired();
		}
	}
}
