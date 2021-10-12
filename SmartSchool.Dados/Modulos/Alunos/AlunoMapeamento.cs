using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSchool.Dominio.Alunos;

namespace SmartSchool.Dados.Modulos.Alunos
{
	public class AlunoMapeamento : IEntityTypeConfiguration<Aluno>
	{
		public void Configure(EntityTypeBuilder<Aluno> builder)
		{
			builder.ToTable("ALUNO");

			builder.Property(b => b.ID)
					.HasColumnName("ALUN_ID_ALUNO")
					.IsRequired();

			builder.Property(b => b.Matricula)
					.HasColumnName("ALUN_COD_ALUNO")
					.ValueGeneratedNever()
					.IsRequired();

			builder.Property(b => b.Ativo)
				   .HasColumnName("ALUN_IN_ATIVO")
				   .IsRequired();

			builder.Property(b => b.Nome)
					.HasColumnName("ALUN_NM_NOME")
					.HasMaxLength(32)
					.IsRequired();

			builder.Property(b => b.Sobrenome)
					.HasColumnName("ALUN_NM_SOBRENOME")
					.HasMaxLength(128)
					.IsRequired();

			builder.Property(b => b.DataNascimento)
					.HasColumnName("ALUN_DT_NASCIMENTO")
					.IsRequired();

			builder.Property(b => b.DataFim)
					.HasColumnName("ALUN_DT_FIM");

			builder.Property(b => b.DataInicio)
					.HasColumnName("ALUN_DT_INICIO")
					.IsRequired();

			builder.Property(b => b.Telefone)
					.HasColumnName("ALUN_NR_TELEFONE")
					.HasMaxLength(16);		   
		}
	}
}
