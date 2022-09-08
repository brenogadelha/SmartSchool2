using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Aplicacao.Semestres.Listar;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dados.Comum;
using SmartSchool.Dados.Contextos;
using SmartSchool.Dados.Modulos.Semestres;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Semestres;
using SmartSchool.Dto.Semestres;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SmartSchool.Testes.API.Controllers.Semestres
{
	public class ListarSemestresTeste : TesteApi
	{
		private readonly IUnidadeDeTrabalho _contextos;
		private readonly IMediator _mediator;

		private readonly SemestreBuilder _semestreBuilder;

		private readonly Semestre _semestre;
		private readonly Semestre _semestre2;
		private readonly Semestre _semestre3;


		public ListarSemestresTeste()
		{
			this._contextos = ContextoFactory.Criar();
			var semestreRepositorio = new SemestreRepositorio(this._contextos);

			this._semestreBuilder = new SemestreBuilder();

			var serviceProvider = GetServiceProviderComMediatR((typeof(IRepositorio<Semestre>), semestreRepositorio));

			this._mediator = serviceProvider.GetRequiredService<IMediator>();

			this._semestre = Semestre.Criar(DateTime.Now.AddDays(10), DateTime.Now.AddMonths(2));
			this._semestre2 = Semestre.Criar(DateTime.Now.AddDays(15), DateTime.Now.AddMonths(3));
			this._semestre3 = Semestre.Criar(DateTime.Now.AddDays(20), DateTime.Now.AddMonths(5));

			this._contextos.SmartContexto.Semestres.Add(this._semestre);
			this._contextos.SmartContexto.Semestres.Add(this._semestre2);
			this._contextos.SmartContexto.Semestres.Add(this._semestre3);
			this._contextos.SmartContexto.SaveChangesAsync();
		}

		[Fact(DisplayName = "Lista Semestres")]
		public async void DeveObterSemestre()
		{
			var retorno = await this._mediator.Send(new ListarSemestresCommand());

			var resultSemestres = retorno.Should().BeOfType<Result<IEnumerable<ObterSemestreDto>>>().Subject;

			resultSemestres.Value.Should().NotBeNull();
			resultSemestres.Value.Count().Should().Be(4);
			resultSemestres.Value.Where(x => x.DataInicio == this._semestre.DataInicio).Count().Should().Be(1);
			resultSemestres.Value.Where(x => x.DataFim == this._semestre2.DataFim).Count().Should().Be(1);
			resultSemestres.Value.Where(x => x.DataFim == this._semestre3.DataFim).Count().Should().Be(1);
			resultSemestres.Value.Where(x => x.DataInicio == this._semestreBuilder.ObterSemestre().DataInicio).Count().Should().Be(1);
		}
	}
}
