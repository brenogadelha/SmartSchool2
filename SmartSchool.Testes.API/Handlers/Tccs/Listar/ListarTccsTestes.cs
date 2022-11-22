using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Aplicacao.Tccs.Listar;
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
using SmartSchool.Testes.API.Controllers.Professores;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SmartSchool.Testes.API.Controllers.Tccs
{
	public class ListarTccsTestes : TesteApi
	{
		private readonly IUnidadeDeTrabalho _contextos;
		private readonly IMediator _mediator;

		private readonly ProfessorBuilder _professorBuilder;

		public ListarTccsTestes()
		{
			this._contextos = ContextoFactory.Criar();
			this._professorBuilder = new ProfessorBuilder();

			var tccRepositorio = new TccRepositorio(this._contextos);
			var professorRepositorio = new ProfessorRepositorio(this._contextos);

			var tccDominio = new TccServicoDominio(tccRepositorio);
			var professorDominio = new ProfessorServicoDominio(professorRepositorio);

			var serviceProvider = GetServiceProviderComMediatR((typeof(IRepositorio<Tcc>), tccRepositorio),
				(typeof(ITccServicoDominio), tccDominio), (typeof(IProfessorServicoDominio), professorDominio));

			this._mediator = serviceProvider.GetRequiredService<IMediator>();

			var professor = this._professorBuilder.ObterProfessor();

			var tcc = Tcc.Criar("Inteligência Artificial", "descrição tema", new List<Guid> { professor.ID });
			var tcc2 = Tcc.Criar("Automação e Visualização de Dados", "descrição tema", new List<Guid> { professor.ID });
			var tcc3 = Tcc.Criar("Metodologia Ágil", "descrição tema", new List<Guid> { professor.ID });

			this._contextos.SmartContexto.Tccs.Add(tcc);
			this._contextos.SmartContexto.Tccs.Add(tcc2);
			this._contextos.SmartContexto.Tccs.Add(tcc3);
			this._contextos.SmartContexto.SaveChangesAsync();
		}

		[Fact(DisplayName = "Obtém Tccs com sucesso")]
		public async void DeveListarTccs()
		{
			var requestTccs = await this._mediator.Send(new ListarTccsQuery());
			var resultTccsObtidos = requestTccs.Should().BeOfType<Result<IEnumerable<ObterTccsDto>>>().Subject;

			resultTccsObtidos.Value.Should().NotBeNull();
			resultTccsObtidos.Value.Count().Should().Be(3);
			resultTccsObtidos.Value.Select(x => x.Id).Should().NotBeNull();
			resultTccsObtidos.Value.Where(x => x.Tema == "Inteligência Artificial").Count().Should().Be(1);
			resultTccsObtidos.Value.Where(x => x.Tema == "Automação e Visualização de Dados").Count().Should().Be(1);
			resultTccsObtidos.Value.Where(x => x.Tema == "Metodologia Ágil").Count().Should().Be(1);
			resultTccsObtidos.Value.Where(x => x.Professor == "Paulo Roberto").Count().Should().Be(3);
		}
	}
}