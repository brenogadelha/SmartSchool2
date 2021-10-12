using SmartSchool.Dominio.Alunos;
using System;

namespace SmartSchool.Dominio.Disciplinas
{
    public class AlunoDisciplina
    {
        private AlunoDisciplina() { }

        public Guid AlunoID { get; private set; }
        public Aluno Aluno { get; private set; }

        public Guid DisciplinaID { get; private set; }
        public Disciplina Disciplina { get; private set; }

        public StatusDisciplinaEnum StatusDisciplina { get; private set; }

        public static AlunoDisciplina Criar(Guid alunoId, Guid disciplinaId, StatusDisciplinaEnum statusDisciplina) => new AlunoDisciplina()
        {
            AlunoID = alunoId,
            DisciplinaID = disciplinaId,
            StatusDisciplina = statusDisciplina
        };
    }
}
