using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSchool.Dominio.Alunos;
using SmartSchool.Dominio.Semestres;

namespace SmartSchool.Dados.Modulos.Semestres
{
	public class SemestreMapeamento : IEntityTypeConfiguration<Semestre>
	{
		public void Configure(EntityTypeBuilder<Semestre> builder)
		{
			builder.ToTable("SEMESTRE");

			builder.Property(b => b.ID)
					.HasColumnName("SEME_ID_SEMESTRE")
					.IsRequired();
			builder.Property(b => b.DataFim)
					.HasColumnName("SEME_DT_FIM");

			builder.Property(b => b.DataInicio)
					.HasColumnName("SEME_DT_INICIO")
					.IsRequired();
		}
	}
}
