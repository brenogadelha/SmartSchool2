using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Aplicacao.Professores.Listar;
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
using SmartSchool.Dto.Dtos.Professores;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SmartSchool.Testes.API.Controllers.Professores
{
	public class ListarProfessoresTestes : TesteApi
	{
		private readonly IUnidadeDeTrabalho _contextos;
		private readonly IMediator _mediator;

		private readonly Disciplina _disciplina;

		private readonly Professor _professor;
		private readonly Professor _professor2;

		private readonly ProfessorBuilder _professorBuilder;

		public ListarProfessoresTestes()
		{
			this._contextos = ContextoFactory.Criar();
			this._professorBuilder = new ProfessorBuilder();

			var professorRepositorio = new ProfessorRepositorio(this._contextos);

			var serviceProvider = GetServiceProviderComMediatR((typeof(IRepositorio<Professor>), professorRepositorio));

			this._mediator = serviceProvider.GetRequiredService<IMediator>();

			// Criação de Disciplinas
			var disciplinaDto1 = new DisciplinaDto() { Nome = "Linguagens Formais e Automatoss", Periodo = 1 };

			this._disciplina = Disciplina.Criar(disciplinaDto1);

			this._professor = Professor.Criar("Estevão jose", 2017100150, new List<Guid> { this._disciplina.ID });
			this._professor2 = Professor.Criar("Luis Roberto", 2017100155, new List<Guid> { this._disciplina.ID });

			this._contextos.SmartContexto.Disciplinas.Add(this._disciplina);
			this._contextos.SmartContexto.Professores.Add(this._professor);
			this._contextos.SmartContexto.Professores.Add(this._professor2);
			this._contextos.SmartContexto.SaveChangesAsync();
		}

		[Fact(DisplayName = "Lista Professores")]
		public async void DeveListarProfessores()
		{
			var professorDto = new ListarProfessoresQuery();

			var retorno = await this._mediator.Send(professorDto);

			var resultProfessores = retorno.Should().BeOfType<Result<IEnumerable<ObterProfessorDto>>>().Subject;

			resultProfessores.Value.Should().NotBeNull();
			resultProfessores.Value.Count().Should().Be(3);
			resultProfessores.Value.Where(x => x.Nome == "Estevão jose").Count().Should().Be(1);
			resultProfessores.Value.Where(x => x.Nome == "Luis Roberto").Count().Should().Be(1);
			resultProfessores.Value.Where(x => x.Nome == "Paulo Roberto").Count().Should().Be(1);
		}
	}
}