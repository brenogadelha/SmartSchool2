using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Aplicacao.Semestres.Adicionar;
using SmartSchool.Aplicacao.Semestres.Alterar;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dados.Comum;
using SmartSchool.Dados.Contextos;
using SmartSchool.Dados.Modulos.Semestres;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Semestres;
using SmartSchool.Dominio.Semestres.Servicos;
using System;
using Xunit;

namespace SmartSchool.Testes.API.Controllers.Semestres
{
	public class AlterarSemestreTeste : TesteApi
	{
		private readonly IUnidadeDeTrabalho _contextos;
		private readonly IMediator _mediator;

		private readonly SemestreBuilder _semestreBuilder;

		public AlterarSemestreTeste()
		{
			this._contextos = ContextoFactory.Criar();
			var semestreRepositorio = new SemestreRepositorio(this._contextos);

			this._semestreBuilder = new SemestreBuilder();

			var semestreServicoDominio = new SemestreServicoDominio(semestreRepositorio);

			var serviceProvider = GetServiceProviderComMediatR((typeof(IRepositorio<Semestre>), semestreRepositorio), (typeof(ISemestreServicoDominio), semestreServicoDominio));

			this._mediator = serviceProvider.GetRequiredService<IMediator>();
		}

		[Fact(DisplayName = "Altera Semestre")]
		public async void DeveAlterarSemestre()
		{
			var semestreDto = new AlterarSemestreCommand() { DataInicio = DateTime.Now, DataFim = DateTime.Now.AddMonths(4), ID = this._semestreBuilder.ObterSemestre().ID };

			var retorno = await this._mediator.Send(semestreDto);

			retorno.Status.Should().Be(Result.Success().Status);
		}
	}
}
