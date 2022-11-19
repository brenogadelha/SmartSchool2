using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Aplicacao.Tccs.ObterPorAluno;
using SmartSchool.Comum.Dominio.Enums;
using SmartSchool.Comum.Enums;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dados.Comum;
using SmartSchool.Dados.Contextos;
using SmartSchool.Dados.Modulos.Tccs;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Tccs;
using SmartSchool.Dto.Tccs;
using SmartSchool.Testes.API.Controllers.Alunos;
using SmartSchool.Testes.API.Controllers.Professores;
using System;
using System.Collections.Generic;
using Xunit;

namespace SmartSchool.Testes.API.Controllers.Tccs
{
	public class ObterTccAlunoTestes : TesteApi
	{
		private readonly IUnidadeDeTrabalho _contextos;
		private readonly IMediator _mediator;

		private readonly AlunoBuilder _alunoBuilder;
		private readonly ProfessorBuilder _professorBuilder;

		public ObterTccAlunoTestes()
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
			this._contextos.SmartContexto.SaveChangesAsync();

			this._mediator = serviceProvider.GetRequiredService<IMediator>();
		}

		[Fact(DisplayName = "Obtém Tcc solicitado por Aluno com sucesso")]
		public async void DeveObterTccSolicitadoPorAluno()
		{
			var retornoSolicitacaoTcc = await this._mediator.Send(new ObterTccPorAlunoQuery
			{
				AlunoId = this._alunoBuilder.ObterAluno().ID
			});

			var resultTccObtidoPorAluno = retornoSolicitacaoTcc.Should().BeOfType<Result<ObterStatusSolicitacaoTccDto>>().Subject;

			resultTccObtidoPorAluno.Value.Should().NotBeNull();
			resultTccObtidoPorAluno.Value.Tema.Should().Be("Inteligência Artificial");
			resultTccObtidoPorAluno.Value.NomeProfessor.Should().Be("Paulo Roberto");
			resultTccObtidoPorAluno.Value.EmailProfessor.Should().Be("pauloroberto@unicarioca.com.br");
			resultTccObtidoPorAluno.Value.Status.Should().Be(TccStatus.Solicitado.Descricao());
			resultTccObtidoPorAluno.Value.DataSolicitacao.ToString().Should().Contain(DateTime.Now.ToString("dd/MM/yyyy"));
		}
	}
}