using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Aplicacao.Professores.Alterar;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dados.Comum;
using SmartSchool.Dados.Contextos;
using SmartSchool.Dados.Modulos.Disciplinas;
using SmartSchool.Dados.Modulos.Professores;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Disciplinas.Servicos;
using SmartSchool.Dominio.Professores;
using SmartSchool.Dominio.Professores.Servicos;
using SmartSchool.Dto.Disciplinas;
using System;
using System.Collections.Generic;
using Xunit;

namespace SmartSchool.Testes.API.Controllers.Professores
{
	public class AlterarProfessorTestes : TesteApi
	{
		private readonly IUnidadeDeTrabalho _contextos;
		private readonly IMediator _mediator;

		private readonly Disciplina _disciplina;
		private readonly Disciplina _disciplina2;

		private readonly ProfessorBuilder _professorBuilder;

		public AlterarProfessorTestes()
		{
			this._contextos = ContextoFactory.Criar();
			this._professorBuilder = new ProfessorBuilder();

			var professorRepositorio = new ProfessorRepositorio(this._contextos);
			var disciplinaRepositorio = new DisciplinaRepositorio(this._contextos);

			var disciplinaServicoDominio = new DisciplinaServicoDominio(disciplinaRepositorio);
			var professorServicoDominio = new ProfessorServicoDominio(professorRepositorio);

			var serviceProvider = GetServiceProviderComMediatR((typeof(IRepositorio<Professor>), professorRepositorio),
				(typeof(IProfessorServicoDominio), professorServicoDominio), (typeof(IDisciplinaServicoDominio), disciplinaServicoDominio));

			this._mediator = serviceProvider.GetRequiredService<IMediator>();

			// Criação de Disciplinas
			var disciplinaDto1 = new DisciplinaDto() { Nome = "Linguagens Formais e Automatoss", Periodo = 1 };
			var disciplinaDto2 = new DisciplinaDto() { Nome = "Teoria em Grafoss", Periodo = 2 };

			this._disciplina = Disciplina.Criar(disciplinaDto1);
			this._disciplina2 = Disciplina.Criar(disciplinaDto2);

			this._contextos.SmartContexto.Disciplinas.Add(this._disciplina);
			this._contextos.SmartContexto.Disciplinas.Add(this._disciplina2);
			this._contextos.SmartContexto.SaveChanges();
		}

		[Fact(DisplayName = "Altera Professor")]
		public async void DeveAlterarProfessor()
		{
			var professor = this._professorBuilder.ObterProfessor();

			var professorDto = new AlterarProfessorCommand() { Matricula = 2017100180, Nome = "Ruan Vojda", Disciplinas = new List<Guid>() { this._disciplina.ID, this._disciplina2.ID }, ID = professor.ID };

			var retorno = await this._mediator.Send(professorDto);

			retorno.Status.Should().Be(Result.Success().Status);
		}
	}
}