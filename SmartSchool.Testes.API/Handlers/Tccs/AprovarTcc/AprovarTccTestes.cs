using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Aplicacao.Tccs.Aprovar;
using SmartSchool.Comum.Dominio.Enums;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dados.Comum;
using SmartSchool.Dados.Contextos;
using SmartSchool.Dados.Modulos.Tccs;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Tccs;
using SmartSchool.Testes.API.Controllers.Alunos;
using SmartSchool.Testes.API.Controllers.Professores;
using System;
using System.Collections.Generic;
using Xunit;

namespace SmartSchool.Testes.API.Controllers.Tccs
{
	public class AprovarTccTestes : TesteApi
	{
		private readonly IUnidadeDeTrabalho _contextos;
		private readonly IMediator _mediator;

		private readonly AlunoBuilder _alunoBuilder;
		private readonly ProfessorBuilder _professorBuilder;

		public AprovarTccTestes()
		{
			this._contextos = ContextoFactory.Criar();
			this._alunoBuilder = new AlunoBuilder();
			this._professorBuilder = new ProfessorBuilder();

			var tccAlunoProfessorRepositorio = new TccAlunoProfessorRepositorio(this._contextos);

			var serviceProvider = GetServiceProviderComMediatR((typeof(IRepositorio<TccAlunoProfessor>), tccAlunoProfessorRepositorio));

			var tcc = Tcc.Criar("Inteligência Artificial", "descrição tema", new List<Guid> { this._professorBuilder.ObterProfessor().ID });

			var tccAlunoProfessor = TccAlunoProfessor.Criar(tcc.Value.ID, this._professorBuilder.ObterProfessor().ID, this._alunoBuilder.ObterAluno().ID, "solicitacao");

			this._contextos.SmartContexto.Tccs.Add(tcc);
			this._contextos.SmartContexto.TccAlunosProfessores.Add(tccAlunoProfessor);
			this._contextos.SmartContexto.SaveChanges();

			this._mediator = serviceProvider.GetRequiredService<IMediator>();
		}

		[Fact(DisplayName = "Aprova Tcc com sucesso")]
		public async void DeveAprovarTcc()
		{
			var aluno = this._alunoBuilder.ObterAluno();

			var retornoSolicitacaoTcc = await this._mediator.Send(new AprovarTccCommand
			{
				AlunoId = this._alunoBuilder.ObterAluno().ID,
				ProfessorId = this._professorBuilder.ObterProfessor().ID,
				RespostaSolicitacao = "Aceito",
				StatusTcc = TccStatus.Aceito
			});

			retornoSolicitacaoTcc.Status.Should().Be(Result.Success().Status);
		}
	}
}