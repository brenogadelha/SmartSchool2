using SmartSchool.Dados.Comum;
using SmartSchool.Dados.Contextos;
using SmartSchool.Dominio.Cursos;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dto.Curso;
using SmartSchool.Dto.Disciplinas;
using System;
using System.Collections.Generic;

namespace SmartSchool.Testes.API.Controllers.Cursos
{
	public class CursoBuilder
	{
		private readonly IUnidadeDeTrabalho _contextos;

		private readonly DisciplinaDto _disciplinaDto1;
		private readonly DisciplinaDto _disciplinaDto2;

		private readonly Disciplina _disciplina1;
		private readonly Disciplina _disciplina2;

		private readonly Curso _curso;

		public CursoBuilder()
		{
			this._contextos = ContextoFactory.Criar();

			// Criação de Disciplinas
			this._disciplinaDto1 = new DisciplinaDto() { Nome = "Linguagens Formais e Automatos", Periodo = 1 };
			this._disciplinaDto2 = new DisciplinaDto() { Nome = "Teoria em Grafos", Periodo = 2 };

			this._disciplina1 = Disciplina.Criar(_disciplinaDto1);
			this._disciplina2 = Disciplina.Criar(_disciplinaDto2);

			this._curso = Curso.Criar(new CursoDto { Nome = "Engenharia da Computação", DisciplinasId = new List<Guid> { this._disciplina1.ID, this._disciplina2.ID } });

			this._contextos.SmartContexto.Disciplinas.Add(this._disciplina1);
			this._contextos.SmartContexto.Disciplinas.Add(this._disciplina2);
			this._contextos.SmartContexto.Cursos.Add(this._curso);
			this._contextos.SmartContexto.SaveChangesAsync();
		}

		public Curso ObterCurso() => this._curso;
	}
}
