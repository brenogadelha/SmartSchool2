using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Aplicacao.Tccs.Remover;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dados.Comum;
using SmartSchool.Dados.Contextos;
using SmartSchool.Dados.Modulos.Professores;
using SmartSchool.Dados.Modulos.Tccs;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Professores.Servicos;
using SmartSchool.Dominio.Tccs;
using SmartSchool.Dominio.Tccs.Servicos;
using Xunit;

namespace SmartSchool.Testes.API.Controllers.Tccs
{
	public class RemoverTccTestes : TesteApi
	{
		private readonly IUnidadeDeTrabalho _contextos;
		private readonly IMediator _mediator;

		private readonly TccBuilder _tccBuilder;

		public RemoverTccTestes()
		{
			this._contextos = ContextoFactory.Criar();
			this._tccBuilder = new TccBuilder();

			var tccRepositorio = new TccRepositorio(this._contextos);
			var professorRepositorio = new ProfessorRepositorio(this._contextos);

			var tccDominio = new TccServicoDominio(tccRepositorio);
			var professorDominio = new ProfessorServicoDominio(professorRepositorio);

			var serviceProvider = GetServiceProviderComMediatR((typeof(IRepositorio<Tcc>), tccRepositorio),
				(typeof(ITccServicoDominio), tccDominio), (typeof(IProfessorServicoDominio), professorDominio));

			this._mediator = serviceProvider.GetRequiredService<IMediator>();
		}

		[Fact(DisplayName = "Remove Tcc com sucesso")]
		public async void DeveRemoverTcc()
		{
			var requestRemoverTcc = await this._mediator.Send(new RemoverTccCommand { ID = this._tccBuilder.ObterTcc().ID });

			requestRemoverTcc.Status.Should().Be(Result.Success().Status);
		}
	}
}