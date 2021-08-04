using SmartSchool.Comum.Dominio;
using SmartSchool.Dto.Dtos.Professores;
using SmartSchool.Dto.Professores;
using System;

namespace SmartSchool.Dominio.Professores
{
    public class Professor : IEntidade
    {
        public Guid ID { get; private set; }
        public int Matricula { get; private set; }
        public string Nome { get; private set; }
        //public IEnumerable<Disciplina> Disciplinas { get; private set; }

        public Professor() { }

        public static Professor Criar(ProfessorDto professorDto)
        {
            var professor = new Professor()
            {
                ID = Guid.NewGuid(),
                Nome = professorDto.Nome,
                Matricula = professorDto.Matricula
            };

            return professor;
        }

        public void AlterarNome(string nome) => this.Nome = nome;
        public void AlterarMatricula(int matricula) => this.Matricula = matricula;
    }
}
