using SmartSchool.Comum.Dominio;
using SmartSchool.Comum.Validacao;
using SmartSchool.Dominio.Alunos;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Disciplinas.Validacao;
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
		public bool Ativo { get; set; }

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
			ValidacaoFabrica.Validar(disciplinaDto, new DisciplinaValidacao());

			var disciplina = new Disciplina()
			{
				ID = Guid.NewGuid(),
				Nome = disciplinaDto.Nome,
				Periodo = (PeriodoDisciplinaEnum)disciplinaDto.Periodo,
				Ativo = true
			};

			return disciplina;
		}

		public static Result<Disciplina> Criar(string nome, int periodo)
		{
			//ValidacaoFabrica.Validar(disciplinaDto, new DisciplinaValidacao());

			var disciplina = new Disciplina()
			{
				ID = Guid.NewGuid(),
				Nome = nome,
				Periodo = (PeriodoDisciplinaEnum)periodo,
				Ativo = true
			};

			return Result<Disciplina>.Success(disciplina);
		}

		public void AlterarNome(string nome) => this.Nome = nome;
		public void AlterarPeriodo(int periodo) => this.Periodo = (PeriodoDisciplinaEnum)periodo;
		public void AlterarAtivo(bool ativo) => this.Ativo = ativo;
	}
}
