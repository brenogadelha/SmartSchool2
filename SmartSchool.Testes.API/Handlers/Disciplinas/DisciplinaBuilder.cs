using SmartSchool.Dados.Comum;
using SmartSchool.Dados.Contextos;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Professores;
using SmartSchool.Dto.Disciplinas;
using System;
using System.Collections.Generic;

namespace SmartSchool.Testes.API.Controllers.Disciplinas
{
	public class DisciplinaBuilder
	{
		private readonly IUnidadeDeTrabalho _contextos;

		private readonly Disciplina _disciplina;

		public DisciplinaBuilder()
		{
			this._contextos = ContextoFactory.Criar();

			// Criação de Disciplinas
			var disciplinaDto = new DisciplinaDto() { Nome = "Linguagens Formais e Automatos", Periodo = 1 };

			this._disciplina = Disciplina.Criar(disciplinaDto);

			var professor = Professor.Criar("José Paulo", 123, "josepaulo@unicarioca.com.br", new List<Guid> { this._disciplina.ID });
			var professor2 = Professor.Criar("Paulo Roberto", 124, "pauloroberto@unicarioca.com.br", new List<Guid> { this._disciplina.ID });

			this._contextos.SmartContexto.Disciplinas.Add(this._disciplina);
			this._contextos.SmartContexto.Professores.Add(professor);
			this._contextos.SmartContexto.Professores.Add(professor2);
			this._contextos.SmartContexto.SaveChangesAsync();
		}

		public Disciplina ObterDisciplina() => this._disciplina;
	}
}
