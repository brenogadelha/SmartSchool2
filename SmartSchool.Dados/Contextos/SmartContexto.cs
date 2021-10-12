using Microsoft.EntityFrameworkCore;
using SmartSchool.Dados.Modulos.Alunos;
using SmartSchool.Dados.Modulos.Professores;
using SmartSchool.Dados.Modulos.Usuarios;
using SmartSchool.Dominio.Alunos;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Professores;
using System;
using System.Collections.Generic;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("SmartSchool");

            modelBuilder.ApplyConfiguration(new AlunoMapeamento());
            modelBuilder.ApplyConfiguration(new ProfessorMapeamento());
            modelBuilder.ApplyConfiguration(new DisciplinaMapeamento());
            modelBuilder.ApplyConfiguration(new AlunoDisciplinaMapeamento());
            modelBuilder.ApplyConfiguration(new ProfessorDisciplinaMapeamento());

            //modelBuilder.Entity<AlunoDisciplina>().HasKey(Ad => new { Ad.AlunoId, Ad.DisciplinaId });

            //modelBuilder.Entity<Professor>()
            //    .HasData(new List<Professor>(){
            //        new Professor(1, "Lauro"),
            //        new Professor(2, "Roberto"),
            //        new Professor(3, "Ronaldo"),
            //        new Professor(4, "Rodrigo"),
            //        new Professor(5, "Alexandre"),
            //    });

            //modelBuilder.Entity<Disciplina>()
            //    .HasData(new List<Disciplina>{
            //        new Disciplina(1, "Matemática", 1),
            //        new Disciplina(2, "Física", 2),
            //        new Disciplina(3, "Português", 3),
            //        new Disciplina(4, "Inglês", 4),
            //        new Disciplina(5, "Programação", 5)
            //    });

            //modelBuilder.Entity<Aluno>()
            //    .HasData(new List<Aluno>(){
            //        new Aluno(1, "Marta", "Kent", "33225555"),
            //        new Aluno(2, "Paula", "Isabela", "3354288"),
            //        new Aluno(3, "Laura", "Antonia", "55668899"),
            //        new Aluno(4, "Luiza", "Maria", "6565659"),
            //        new Aluno(5, "Lucas", "Machado", "565685415"),
            //        new Aluno(6, "Pedro", "Alvares", "456454545"),
            //        new Aluno(7, "Paulo", "José", "9874512")
            //    });

            //modelBuilder.Entity<AlunoDisciplina>()
            //    .HasData(new List<AlunoDisciplina>() {
            //        new AlunoDisciplina() {AlunoId = 1, DisciplinaId = 2 },
            //        new AlunoDisciplina() {AlunoId = 1, DisciplinaId = 4 },
            //        new AlunoDisciplina() {AlunoId = 1, DisciplinaId = 5 },
            //        new AlunoDisciplina() {AlunoId = 2, DisciplinaId = 1 },
            //        new AlunoDisciplina() {AlunoId = 2, DisciplinaId = 2 },
            //        new AlunoDisciplina() {AlunoId = 2, DisciplinaId = 5 },
            //        new AlunoDisciplina() {AlunoId = 3, DisciplinaId = 1 },
            //        new AlunoDisciplina() {AlunoId = 3, DisciplinaId = 2 },
            //        new AlunoDisciplina() {AlunoId = 3, DisciplinaId = 3 },
            //        new AlunoDisciplina() {AlunoId = 4, DisciplinaId = 1 },
            //        new AlunoDisciplina() {AlunoId = 4, DisciplinaId = 4 },
            //        new AlunoDisciplina() {AlunoId = 4, DisciplinaId = 5 },
            //        new AlunoDisciplina() {AlunoId = 5, DisciplinaId = 4 },
            //        new AlunoDisciplina() {AlunoId = 5, DisciplinaId = 5 },
            //        new AlunoDisciplina() {AlunoId = 6, DisciplinaId = 1 },
            //        new AlunoDisciplina() {AlunoId = 6, DisciplinaId = 2 },
            //        new AlunoDisciplina() {AlunoId = 6, DisciplinaId = 3 },
            //        new AlunoDisciplina() {AlunoId = 6, DisciplinaId = 4 },
            //        new AlunoDisciplina() {AlunoId = 7, DisciplinaId = 1 },
            //        new AlunoDisciplina() {AlunoId = 7, DisciplinaId = 2 },
            //        new AlunoDisciplina() {AlunoId = 7, DisciplinaId = 3 },
            //        new AlunoDisciplina() {AlunoId = 7, DisciplinaId = 4 },
            //        new AlunoDisciplina() {AlunoId = 7, DisciplinaId = 5 }
            //    });
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
