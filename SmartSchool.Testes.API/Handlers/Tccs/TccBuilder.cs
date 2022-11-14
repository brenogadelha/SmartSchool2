using SmartSchool.Comum.Dominio.Enums;
using SmartSchool.Dados.Comum;
using SmartSchool.Dados.Contextos;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Professores;
using SmartSchool.Dominio.Tccs;
using System;
using System.Collections.Generic;

namespace SmartSchool.Testes.API.Controllers.Tccs
{
	public class TccBuilder
	{
		private readonly IUnidadeDeTrabalho _contextos;

		private readonly Disciplina _disciplina;
		private readonly Professor _professor;

		private readonly Tcc _tcc;

		public TccBuilder()
		{
			this._contextos = ContextoFactory.Criar();

			// Criação de Professor
			this._disciplina = Disciplina.Criar("Linguagens Formais e Automatos", 4);
			this._professor = Professor.Criar("Paulo Augusto", 1, "pauloaugusto@unicarioca.com.br", new List<Guid> { this._disciplina.ID }, DisponibilidadeTcc.Disponível);

			this._tcc = Tcc.Criar("Robótica", "descrição tema", new List<Guid> { this._professor.ID });

			this._contextos.SmartContexto.Disciplinas.Add(this._disciplina);
			this._contextos.SmartContexto.Professores.Add(this._professor);
			this._contextos.SmartContexto.Tccs.Add(this._tcc);
			this._contextos.SmartContexto.SaveChangesAsync();
		}

		public Tcc ObterTcc() => this._tcc;
	}
}
