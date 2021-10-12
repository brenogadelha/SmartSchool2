using SmartSchool.Dominio.Disciplinas;
using System;

namespace SmartSchool.Dominio.Professores
{
    public class ProfessorDisciplina
    {
        private ProfessorDisciplina() { }

        public Guid ProfessorID { get; private set; }
        public Professor Professor { get; private set; }

        public Guid DisciplinaID { get; private set; }
        public Disciplina Disciplina { get; private set; }

        public static ProfessorDisciplina Criar(Guid professorId, Guid disciplinaId) => new ProfessorDisciplina()
        {
            ProfessorID = professorId,
            DisciplinaID = disciplinaId
        };
    }
}
