using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Aplicacao.Disciplinas.ObterPorId;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dados.Comum;
using SmartSchool.Dados.Contextos;
using SmartSchool.Dados.Modulos.Disciplinas;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Disciplinas.Servicos;
using SmartSchool.Dto.Disciplinas.Obter;
using System;
using Xunit;

namespace SmartSchool.Testes.API.Controllers.Disciplinas
{
	public class ObterDisciplinaTestes : TesteApi
	{
		private readonly IUnidadeDeTrabalho _contextos;
		private readonly IMediator _mediator;

		private readonly DisciplinaBuilder _disciplinaBuilder;

		public ObterDisciplinaTestes()
		{
			this._contextos = ContextoFactory.Criar();
			this._disciplinaBuilder = new DisciplinaBuilder();

			var disciplinaRepositorio = new DisciplinaRepositorio(this._contextos);

			var disciplinaServico = new DisciplinaServicoDominio(disciplinaRepositorio);

			var serviceProvider = GetServiceProviderComMediatR((typeof(IRepositorio<Disciplina>), disciplinaRepositorio), (typeof(IDisciplinaServicoDominio), disciplinaServico));

			this._mediator = serviceProvider.GetRequiredService<IMediator>();
		}

		[Fact(DisplayName = "Obtém Disciplina")]
		public async void DeveObterDisciplina()
		{
			var disciplina = this._disciplinaBuilder.ObterDisciplina();

			var retorno = await this._mediator.Send(new ObterDisciplinaQuery { Id = disciplina.ID });

			var resultDisciplinaObtidaPorId = retorno.Should().BeOfType<Result<ObterDisciplinaDto>>().Subject;

			resultDisciplinaObtidaPorId.Value.Should().NotBeNull();
			resultDisciplinaObtidaPorId.Value.ID.Should().NotBe(Guid.Empty);
			resultDisciplinaObtidaPorId.Value.Nome.Should().Be(disciplina.Nome);
			resultDisciplinaObtidaPorId.Value.Periodo.Should().Be((int)disciplina.Periodo);
		}
	}
}