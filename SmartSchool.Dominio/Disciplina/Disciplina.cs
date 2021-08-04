using SmartSchool.Comum.Dominio;
using SmartSchool.Dto.Disciplinas;
using System;

namespace SmartSchool.Dominio.Disciplinas
{
    public class Disciplina : IEntidade
    {
        public Guid ID { get; private set; }
        public string Nome { get; private set; }
        //public Professor Professor { get; private set; }
        //public IEnumerable<AlunoDisciplina> AlunosDisciplinas { get; set; }

        public Disciplina() { }
        public static Disciplina Criar(DisciplinaDto disciplinaDto)
        {
            var disciplina = new Disciplina()
            {
                ID = Guid.NewGuid(),
                Nome = disciplinaDto.Nome,
            };

            return disciplina;
        }

        public void AlterarNome(string nome) => this.Nome = nome;
    }
}
