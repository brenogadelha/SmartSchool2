using SmartSchool.Comum.Dominio;
using SmartSchool.Dominio.Disciplinas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.Dominio.Professores
{
    public class Professor : IEntidade
    {
        public Guid ID { get; private set; }
        public int Matricula { get; private set; }
        public string Nome { get; private set; }
        public IEnumerable<Disciplina> Disciplinas { get; private set; }

        public Professor() { }
        public Professor(Guid id, string nome) 
        {
            this.ID = id;
            this.Nome = nome;
        }
    }
}
