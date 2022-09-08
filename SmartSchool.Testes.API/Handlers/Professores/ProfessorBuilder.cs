using SmartSchool.Dados.Comum;
using SmartSchool.Dados.Contextos;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Professores;
using SmartSchool.Dto.Disciplinas;
using System;
using System.Collections.Generic;

namespace SmartSchool.Testes.API.Controllers.Professores
{
	public class ProfessorBuilder
	{
		private readonly IUnidadeDeTrabalho _contextos;

		private readonly Disciplina _disciplina;
		private readonly Disciplina _disciplina2;
		private readonly Disciplina _disciplina3;

		private readonly Professor _professor;

		public ProfessorBuilder()
		{
			this._contextos = ContextoFactory.Criar();

			// Criação de Disciplinas
			var disciplinaDto1 = new DisciplinaDto() { Nome = "Linguagens Formais e Automatoss", Periodo = 1 };
			var disciplinaDto2 = new DisciplinaDto() { Nome = "Teoria em Grafoss", Periodo = 2 };
			var disciplinaDto3 = new DisciplinaDto() { Nome = "Projeto Integradorr", Periodo = 3 };

			this._disciplina = Disciplina.Criar(disciplinaDto1);
			this._disciplina2 = Disciplina.Criar(disciplinaDto2);
			this._disciplina3 = Disciplina.Criar(disciplinaDto3);

			this._professor = Professor.Criar("Paulo Roberto", 2017100150, new List<Guid> { this._disciplina.ID, this._disciplina2.ID, this._disciplina3.ID });

			this._contextos.SmartContexto.Disciplinas.Add(this._disciplina);
			this._contextos.SmartContexto.Disciplinas.Add(this._disciplina2);
			this._contextos.SmartContexto.Disciplinas.Add(this._disciplina3);
			this._contextos.SmartContexto.Professores.Add(this._professor);
			this._contextos.SmartContexto.SaveChangesAsync();
		}

		public Professor ObterProfessor() => this._professor;
	}
}
