using SmartSchool.Comum.Dominio;
using SmartSchool.Comum.Dominio.Enums;
using SmartSchool.Comum.Validacao;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Professores.Validacao;
using SmartSchool.Dominio.Tccs;
using SmartSchool.Dto.Professores;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace SmartSchool.Dominio.Professores
{
	public class Professor : IEntidade
	{
		public Guid ID { get; private set; }
		public int Matricula { get; private set; }
		public string Nome { get; private set; }
		public bool Ativo { get; set; }
		public DisponibilidadeTcc DisponibilidadeTcc { get; set; }

		[JsonIgnore]
		public List<ProfessorDisciplina> ProfessoresDisciplinas { get; private set; } = new List<ProfessorDisciplina>();

		[JsonIgnore]
		public List<TccProfessor> Tccs { get; private set; } = new List<TccProfessor>();

		[NotMapped]
		public List<Disciplina> Disciplinas
		{
			get => this.ProfessoresDisciplinas.Select(u => u.Disciplina).ToList();
		}

		public Professor() { }

		public static Professor Criar(ProfessorDto professorDto)
		{
			ValidacaoFabrica.Validar(professorDto, new ProfessorValidacao());

			//var professor = new Professor()
			//         {
			//             ID = Guid.NewGuid(),
			//             Nome = professorDto.Nome,
			//             Matricula = professorDto.Matricula,
			//	Ativo = true
			//};

			//professor.AtualizarDisciplinas(professorDto.Disciplinas);

			//return professor;

			return Criar(professorDto.Nome, professorDto.Matricula, professorDto.Disciplinas);
		}

		public static Result<Professor> Criar(string nome, int matricula, List<Guid> disciplinas)
		{
			var professor = new Professor()
			{
				ID = Guid.NewGuid(),
				Nome = nome,
				Matricula = matricula,
				Ativo = true
			};

			professor.AtualizarDisciplinas(disciplinas);

			return Result<Professor>.Success(professor);
		}

		public void AlterarNome(string nome) => this.Nome = nome;
		public void AlterarMatricula(int matricula) => this.Matricula = matricula;
		public void AlterarAtivo(bool ativo) => this.Ativo = ativo;
		public void AlterarDisponibilidadeTcc(DisponibilidadeTcc disponibilidadeTcc) => this.DisponibilidadeTcc = disponibilidadeTcc;
		public void AtualizarDisciplinas(List<Guid> novasDisciplinas)
		{
			// Verifica se foram incluídas novas Disciplinas. Caso não, são removidas as atuais.
			if (novasDisciplinas == null || !novasDisciplinas.Any())
			{
				this.ProfessoresDisciplinas.Clear();
				return;
			}

			// Excluir do Curso as Disciplinas que não estão presentes na nova lista
			if (this.Disciplinas != null && this.Disciplinas.Any())
				for (int i = this.Disciplinas.Count - 1; i > -1; i--)
				{
					if (!novasDisciplinas.Any(idNovo => idNovo == this.Disciplinas[i].ID))
					{
						this.ProfessoresDisciplinas.Remove(this.ProfessoresDisciplinas.FirstOrDefault(p => p.DisciplinaID == this.Disciplinas[i].ID));
					}
				}

			List<ProfessorDisciplina> listaTemp = new List<ProfessorDisciplina>();

			// Adicionar ao Professor as Disciplinas da lista que são diferentes das atuais
			foreach (Guid id in novasDisciplinas)
				if (!this.Disciplinas.Any(l => l.ID == id))
				{
					listaTemp.Add(ProfessorDisciplina.Criar(this.ID, id));
				}

			this.ProfessoresDisciplinas.AddRange(listaTemp);
		}
	}
}

