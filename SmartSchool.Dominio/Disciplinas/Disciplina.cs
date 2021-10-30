using SmartSchool.Comum.Dominio;
using SmartSchool.Dominio.Alunos;
using SmartSchool.Dominio.Professores;
using SmartSchool.Dto.Disciplinas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace SmartSchool.Dominio.Disciplinas
{
	public class Disciplina : IEntidade
	{
		public Guid ID { get; private set; }
		public string Nome { get; private set; }
		public PeriodoDisciplinaEnum Periodo { get; private set; }

		[JsonIgnore]
		public List<AlunoDisciplina> Alunos { get; private set; } = new List<AlunoDisciplina>();

		[JsonIgnore]
		public List<ProfessorDisciplina> ProfessoresDisciplinas { get; private set; } = new List<ProfessorDisciplina>();

		[NotMapped]
		public List<Professor> Professores
		{
			get => this.ProfessoresDisciplinas.Select(u => u.Professor).ToList();
		}

		[JsonIgnore]
		public List<CursoDisciplina> Cursos { get; private set; } = new List<CursoDisciplina>();

		public Disciplina() { }
		public static Disciplina Criar(DisciplinaDto disciplinaDto)
		{
			var disciplina = new Disciplina()
			{
				ID = Guid.NewGuid(),
				Nome = disciplinaDto.Nome,
				Periodo = (PeriodoDisciplinaEnum)disciplinaDto.Periodo
			};

			return disciplina;
		}

		public void AlterarNome(string nome) => this.Nome = nome;
	}
}
