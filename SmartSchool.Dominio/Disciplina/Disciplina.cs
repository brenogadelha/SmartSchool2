using SmartSchool.Comum.Dominio;
using SmartSchool.Dominio.Alunos;
using SmartSchool.Dominio.Professores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.Dominio.Disciplinas
{
    public class Disciplina : IEntidade
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public int ProfessorId { get; private set; }
        public Professor Professor { get; private set; }
        //public IEnumerable<AlunoDisciplina> AlunosDisciplinas { get; set; }

        public Disciplina() { }
        public Disciplina(int id, string nome, int professorId)
        {
            this.Id = id;
            this.Nome = nome;
            this.ProfessorId = professorId;
        }
    }
}
