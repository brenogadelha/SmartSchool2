using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Aplicacao.Semestres.Remover;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dados.Comum;
using SmartSchool.Dados.Contextos;
using SmartSchool.Dados.Modulos.Semestres;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Semestres;
using SmartSchool.Dominio.Semestres.Servicos;
using Xunit;

namespace SmartSchool.Testes.API.Controllers.Semestres
{
	public class RemoverSemestreTeste : TesteApi
	{
		private readonly IUnidadeDeTrabalho _contextos;
		private readonly IMediator _mediator;

		private readonly SemestreBuilder _semestreBuilder;

		public RemoverSemestreTeste()
		{
			this._contextos = ContextoFactory.Criar();
			var semestreRepositorio = new SemestreRepositorio(this._contextos);

			this._semestreBuilder = new SemestreBuilder();

			var semestreServicoDominio = new SemestreServicoDominio(semestreRepositorio);

			var serviceProvider = GetServiceProviderComMediatR((typeof(IRepositorio<Semestre>), semestreRepositorio), (typeof(ISemestreServicoDominio), semestreServicoDominio));

			this._mediator = serviceProvider.GetRequiredService<IMediator>();
		}

		[Fact(DisplayName = "Remove Semestre")]
		public async void DeveRemoverSemestre()
		{
			var semestreDto = new RemoverSemestreCommand { ID = this._semestreBuilder.ObterSemestre().ID };

			var retorno = await this._mediator.Send(semestreDto);

			retorno.Status.Should().Be(Result.Success().Status);
		}
	}
}
