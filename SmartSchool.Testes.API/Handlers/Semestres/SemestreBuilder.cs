using SmartSchool.Dados.Comum;
using SmartSchool.Dados.Contextos;
using SmartSchool.Dominio.Semestres;
using System;

namespace SmartSchool.Testes.API.Controllers.Semestres
{
	public class SemestreBuilder
	{
		private readonly IUnidadeDeTrabalho _contextos;

		private readonly Semestre _semestre;

		public SemestreBuilder()
		{
			this._contextos = ContextoFactory.Criar();

			// Criação de Semestre

			this._semestre = Semestre.Criar(DateTime.Now, DateTime.Now.AddYears(4));

			this._contextos.SmartContexto.Semestres.Add(this._semestre);
			this._contextos.SmartContexto.SaveChangesAsync();
		}

		public Semestre ObterSemestre() => this._semestre;
	}
}
