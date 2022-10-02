using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Aplicacao.Tccs.ObterPorId;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dados.Comum;
using SmartSchool.Dados.Contextos;
using SmartSchool.Dados.Modulos.Professores;
using SmartSchool.Dados.Modulos.Tccs;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Professores.Servicos;
using SmartSchool.Dominio.Tccs;
using SmartSchool.Dominio.Tccs.Servicos;
using SmartSchool.Dto.Tccs;
using System;
using System.Linq;
using Xunit;

namespace SmartSchool.Testes.API.Controllers.Tccs
{
	public class ObterTccTestes : TesteApi
	{
		private readonly IUnidadeDeTrabalho _contextos;
		private readonly IMediator _mediator;

		private readonly TccBuilder _tccBuilder;

		public ObterTccTestes()
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

		[Fact(DisplayName = "Obtém Tcc com sucesso")]
		public async void DeveObterTcc()
		{
			var tcc = this._tccBuilder.ObterTcc();

			var retornoAlteracao = await this._mediator.Send(new ObterTccCommand { Id = tcc.ID });
			var resultTccObtidoPorId = retornoAlteracao.Should().BeOfType<Result<ObterTccDto>>().Subject;

			resultTccObtidoPorId.Value.Should().NotBeNull();
			resultTccObtidoPorId.Value.Id.Should().NotBe(Guid.Empty);
			resultTccObtidoPorId.Value.Tema.Should().Be(tcc.Tema);
			resultTccObtidoPorId.Value.Descricao.Should().Be(tcc.Descricao);
			resultTccObtidoPorId.Value.Professores.Where(x => x.Nome == "Paulo Augusto").Count().Should().Be(1);
		}
	}
}