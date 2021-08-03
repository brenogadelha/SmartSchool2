using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartSchool.Dominio.Alunos;
using SmartSchool.Dominio.Disciplinas;

namespace SmartSchool.Dados.Modulos.Usuarios
{
	public class DisciplinaMapeamento : IEntityTypeConfiguration<Disciplina>
	{
		public void Configure(EntityTypeBuilder<Disciplina> builder)
		{
			builder.ToTable("DISCIPLINA");

			builder.Property(b => b.Id)
					.HasColumnName("DISC_ID_DISCIPLINA")
					.ValueGeneratedNever()
					.IsRequired();

			builder.Property(b => b.Nome)
					.HasColumnName("DISC_NM_NOME")
					.HasMaxLength(32)
					.IsRequired();

			builder.Property(b => b.ProfessorId)
					.HasColumnName("DISC_ID_PROFESSOR")
					.HasMaxLength(128);	   
		}
	}
}
