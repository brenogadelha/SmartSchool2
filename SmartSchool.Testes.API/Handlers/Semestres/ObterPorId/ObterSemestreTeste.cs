using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Aplicacao.Semestres.ObterPorId;
using SmartSchool.Dados.Comum;
using SmartSchool.Dados.Contextos;
using SmartSchool.Dados.Modulos.Semestres;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Semestres.Servicos;
using SmartSchool.Dto.Semestres;
using Xunit;

namespace SmartSchool.Testes.API.Controllers.Semestres
{
	public class ObterSemestreTeste : TesteApi
	{
		private readonly IUnidadeDeTrabalho _contextos;
		private readonly IMediator _mediator;

		private readonly SemestreBuilder _semestreBuilder;

		public ObterSemestreTeste()
		{
			this._contextos = ContextoFactory.Criar();
			var semestreRepositorio = new SemestreRepositorio(this._contextos);

			this._semestreBuilder = new SemestreBuilder();

			var semestreServicoDominio = new SemestreServicoDominio(semestreRepositorio);

			var serviceProvider = GetServiceProviderComMediatR((typeof(ISemestreServicoDominio), semestreServicoDominio));

			this._mediator = serviceProvider.GetRequiredService<IMediator>();
		}

		[Fact(DisplayName = "Obtém Semestre")]
		public async void DeveObterSemestre()
		{
			var semestre = this._semestreBuilder.ObterSemestre();

			var semestreDto = new ObterSemestreCommand { Id = semestre.ID };

			var retorno = await this._mediator.Send(semestreDto);

			var resultSemestreObtidoPorId = retorno.Should().BeOfType<Result<ObterSemestreDto>>().Subject;

			resultSemestreObtidoPorId.Value.Should().NotBeNull();
			resultSemestreObtidoPorId.Value.DataInicio.ToString().Should().Contain(semestre.DataInicio.ToString());
			resultSemestreObtidoPorId.Value.DataFim.ToString().Should().Contain(semestre.DataFim.ToString());
		}
	}
}
