using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Aplicacao.Tccs.Solicitar;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dados.Comum;
using SmartSchool.Dados.Contextos;
using SmartSchool.Dados.Modulos.Alunos;
using SmartSchool.Dados.Modulos.Professores;
using SmartSchool.Dados.Modulos.Tccs;
using SmartSchool.Dominio.Alunos;
using SmartSchool.Dominio.Alunos.Servicos;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Professores.Servicos;
using SmartSchool.Dominio.Tccs.Servicos;
using SmartSchool.Testes.API.Controllers.Alunos;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SmartSchool.Testes.API.Controllers.Tccs
{
	public class SolicitarTccTestes : TesteApi
	{
		private readonly IUnidadeDeTrabalho _contextos;
		private readonly IMediator _mediator;

		private readonly TccBuilder _tccBuilder;
		private readonly AlunoBuilder _alunoBuilder;

		public SolicitarTccTestes()
		{
			this._contextos = ContextoFactory.Criar();
			this._tccBuilder = new TccBuilder();
			this._alunoBuilder = new AlunoBuilder();

			var tccRepositorio = new TccRepositorio(this._contextos);
			var alunoRepositorio = new AlunoRepositorio(this._contextos);
			var professorRepositorio = new ProfessorRepositorio(this._contextos);

			var tccDominio = new TccServicoDominio(tccRepositorio);
			var professorDominio = new ProfessorServicoDominio(professorRepositorio);
			var alunoDominio = new AlunoServicoDominio(alunoRepositorio);

			var serviceProvider = GetServiceProviderComMediatR((typeof(IRepositorio<Aluno>), alunoRepositorio), (typeof(ITccServicoDominio), tccDominio), 
				(typeof(IProfessorServicoDominio), professorDominio), (typeof(IAlunoServicoDominio), alunoDominio));

			this._mediator = serviceProvider.GetRequiredService<IMediator>();
		}

		[Fact(DisplayName = "Solicita Tcc com sucesso")]
		public async void DeveSolicitarTcc()
		{
			var tcc = this._tccBuilder.ObterTcc();
			var aluno = this._alunoBuilder.ObterAluno();

			var retornoSolicitacaoTcc = await this._mediator.Send(new SolicitarTccCommand { AlunosIds = new List<Guid> { aluno.ID }, ProfessorId = tcc.Professores.FirstOrDefault().ID, TccId = tcc.ID });

			retornoSolicitacaoTcc.Status.Should().Be(Result.Success().Status);
		}
	}
}