using Microsoft.EntityFrameworkCore;
using SmartSchool.Dados.Modulos.Alunos;
using SmartSchool.Dados.Modulos.Professores;
using SmartSchool.Dados.Modulos.Semestres;
using SmartSchool.Dados.Modulos.Tccs;
using SmartSchool.Dados.Modulos.Usuarios;
using SmartSchool.Dominio.Alunos;
using SmartSchool.Dominio.Cursos;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Professores;
using SmartSchool.Dominio.Semestres;
using SmartSchool.Dominio.Tccs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSchool.Dados.Contextos
{
	public class SmartContexto : DbContext
	{
		public SmartContexto(DbContextOptions<SmartContexto> options) : base(options) { }

		public DbSet<Aluno> Alunos { get; set; }
		public DbSet<Disciplina> Disciplinas { get; set; }
		public DbSet<Professor> Professores { get; set; }
		public DbSet<Semestre> Semestres { get; set; }
		public DbSet<Curso> Cursos { get; set; }
		public DbSet<Tcc> Tccs { get; set; }
		public DbSet<TccAlunoProfessor> TccAlunosProfessores { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema("SmartSchool");

			modelBuilder.ApplyConfiguration(new AlunoMapeamento());
			modelBuilder.ApplyConfiguration(new ProfessorMapeamento());
			modelBuilder.ApplyConfiguration(new DisciplinaMapeamento());
			modelBuilder.ApplyConfiguration(new AlunoDisciplinaMapeamento());
			modelBuilder.ApplyConfiguration(new ProfessorDisciplinaMapeamento());
			modelBuilder.ApplyConfiguration(new CursoMapeamento());
			modelBuilder.ApplyConfiguration(new CursoDisciplinaMapeamento());
			modelBuilder.ApplyConfiguration(new SemestreAlunoDisciplinaMapeamento());
			modelBuilder.ApplyConfiguration(new SemestreMapeamento());
			modelBuilder.ApplyConfiguration(new TccMapeamento());
			modelBuilder.ApplyConfiguration(new TccProfessorMapeamento());
			modelBuilder.ApplyConfiguration(new TccAlunoProfessorMapeamento());
		}

		public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
			=> await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken).ConfigureAwait(false);

		public override int SaveChanges()
		{
			try
			{
				return base.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				throw new Exception("Outro usuário tentou editar o mesmo registro que você. Aguarde alguns minutos e tente novamente.");
			}
		}
	}
}
