using SmartSchool.Comum.Dominio;
using SmartSchool.Comum.Validacao;
using SmartSchool.Dominio.Alunos;
using SmartSchool.Dominio.Cursos.Validacao;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dto.Curso;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace SmartSchool.Dominio.Cursos
{
	public class Curso : IEntidade
	{
		public Guid ID { get; private set; }

		public string Nome { get; private set; }

		[JsonIgnore]
		public List<CursoDisciplina> CursosDisciplinas { get; private set; } = new List<CursoDisciplina>();

		[NotMapped]
		public List<Disciplina> Disciplinas
		{
			get => this.CursosDisciplinas.Select(u => u.Disciplina).ToList();
		}

		[JsonIgnore]
		public List<Aluno> Alunos { get; private set; } = new List<Aluno>();

		public static Curso Criar(CursoDto cursoDto)
		{
			ValidacaoFabrica.Validar(cursoDto, new CursoValidacao());

			var curso = new Curso()
			{
				ID = Guid.NewGuid(),
				Nome = cursoDto.Nome
			};

			curso.AtualizarDisciplinas(cursoDto.DisciplinasId);

			return curso;
		}

		public void AlterarNome(string nome) => this.Nome = nome;

		public void AtualizarDisciplinas(List<Guid> novasDisciplinas)
		{
			// Verifica se foram incluídas novas Disciplinas. Caso não, são removidas as atuais.
			if (novasDisciplinas == null || !novasDisciplinas.Any())
			{
				this.CursosDisciplinas.Clear();
				return;
			}

			// Excluir do Curso as Disciplinas que não estão presentes na nova lista
			if (this.Disciplinas != null && this.Disciplinas.Any())
				for (int i = this.Disciplinas.Count - 1; i > -1; i--)
				{
					if (!novasDisciplinas.Any(idNovo => idNovo == this.Disciplinas[i].ID))
					{
						this.CursosDisciplinas.Remove(this.CursosDisciplinas.FirstOrDefault(p => p.DisciplinaID == this.Disciplinas[i].ID));
					}
				}

			List<CursoDisciplina> listaTemp = new List<CursoDisciplina>();

			// Adicionar ao Curso as Disciplinas da lista que são diferentes das atuais
			foreach (Guid id in novasDisciplinas)
				if (!this.Disciplinas.Any(l => l.ID == id))
				{
					listaTemp.Add(CursoDisciplina.Criar(this.ID, id));
				}

			this.CursosDisciplinas.AddRange(listaTemp);
		}
	}
}

