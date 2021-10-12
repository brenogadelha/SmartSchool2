using SmartSchool.Comum.Dominio;
using SmartSchool.Dominio.Professores;
using SmartSchool.Dto.Disciplinas;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SmartSchool.Dominio.Disciplinas
{
    public class Disciplina : IEntidade
    {
        public Guid ID { get; private set; }
        public string Nome { get; private set; }

        [JsonIgnore]
        public List<AlunoDisciplina> Alunos { get; private set; } = new List<AlunoDisciplina>();

        [JsonIgnore]
        public List<ProfessorDisciplina> Professores { get; private set; } = new List<ProfessorDisciplina>();

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
