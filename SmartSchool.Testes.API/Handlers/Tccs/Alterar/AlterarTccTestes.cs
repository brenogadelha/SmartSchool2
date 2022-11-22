using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Aplicacao.Tccs.Alterar;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dados.Comum;
using SmartSchool.Dados.Contextos;
using SmartSchool.Dados.Modulos.Professores;
using SmartSchool.Dados.Modulos.Tccs;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Professores.Servicos;
using SmartSchool.Dominio.Tccs;
using SmartSchool.Dominio.Tccs.Servicos;
using SmartSchool.Testes.API.Controllers.Professores;
using System;
using System.Collections.Generic;
using Xunit;

namespace SmartSchool.Testes.API.Controllers.Tccs
{
	public class AlterarTccTestes : TesteApi
	{
		private readonly IUnidadeDeTrabalho _contextos;
		private readonly IMediator _mediator;

		private readonly TccBuilder _tccBuilder;
		private readonly ProfessorBuilder _professorBuilder;

		public AlterarTccTestes()
		{
			this._contextos = ContextoFactory.Criar();
			this._tccBuilder = new TccBuilder();
			this._professorBuilder = new ProfessorBuilder();

			var tccRepositorio = new TccRepositorio(this._contextos);
			var tccAlunoProfessorRepositorio = new TccAlunoProfessorRepositorio(this._contextos);
			var professorRepositorio = new ProfessorRepositorio(this._contextos);

			var tccDominio = new TccServicoDominio(tccRepositorio);
			var professorDominio = new ProfessorServicoDominio(professorRepositorio);

			var serviceProvider = GetServiceProviderComMediatR((typeof(IRepositorio<Tcc>), tccRepositorio), (typeof(IRepositorio<TccAlunoProfessor>), tccAlunoProfessorRepositorio),
				(typeof(ITccServicoDominio), tccDominio), (typeof(IProfessorServicoDominio), professorDominio));

			this._mediator = serviceProvider.GetRequiredService<IMediator>();
		}

		[Fact(DisplayName = "Altera Tcc com sucesso")]
		public async void DeveAlterarTcc()
		{
			var tcc = this._tccBuilder.ObterTcc();

			// instancia alteração	
			var tccAlteracaoCommand = new AlterarTccCommand() { ID = tcc.ID, Tema = "Visualização de Dados e Automação", Descricao = "descrição tema", Professores = new List<Guid> { this._professorBuilder.ObterProfessor().ID } };

			var retornoAlteracao = await this._mediator.Send(tccAlteracaoCommand);

			retornoAlteracao.Status.Should().Be(Result.Success().Status);
		}
	}
}