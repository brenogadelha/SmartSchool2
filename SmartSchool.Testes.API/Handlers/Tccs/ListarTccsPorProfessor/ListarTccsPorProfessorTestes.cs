using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Aplicacao.Tccs.ListarPorProfessor;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dados.Comum;
using SmartSchool.Dados.Contextos;
using SmartSchool.Dados.Modulos.Tccs;
using SmartSchool.Dominio.Alunos;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Cursos;
using SmartSchool.Dominio.Tccs;
using SmartSchool.Dto.Tccs;
using SmartSchool.Testes.API.Controllers.Alunos;
using SmartSchool.Testes.API.Controllers.Disciplinas;
using SmartSchool.Testes.API.Controllers.Professores;
using SmartSchool.Testes.Compartilhado.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SmartSchool.Testes.API.Controllers.Tccs
{
	public class ListarTccsPorProfessorTestes : TesteApi
	{
		private readonly IUnidadeDeTrabalho _contextos;
		private readonly IMediator _mediator;

		private readonly AlunoDtoBuilder _alunoDtoBuilder;
		private readonly AlunoBuilder _alunoBuilder;
		private readonly ProfessorBuilder _professorBuilder;
		private readonly DisciplinaBuilder _disciplinaBuilder;

		private readonly Curso _curso;
		private readonly Aluno _aluno;
		private readonly Tcc _tcc;

		public ListarTccsPorProfessorTestes()
		{
			this._contextos = ContextoFactory.Criar();
			this._alunoBuilder = new AlunoBuilder();
			this._professorBuilder = new ProfessorBuilder();
			this._disciplinaBuilder = new DisciplinaBuilder();

			var tccAlunoProfessorRepositorio = new TccAlunoProfessorRepositorio(this._contextos);

			var serviceProvider = GetServiceProviderComMediatR((typeof(IRepositorio<TccAlunoProfessor>), tccAlunoProfessorRepositorio));

			this._curso = Curso.Criar("Engenharia da Computação", new List<Guid> { this._disciplinaBuilder.ObterDisciplina().ID });

			this._alunoDtoBuilder = AlunoDtoBuilder.Novo
				.ComCidade("Rio de Janeiro")
				.ComMatricula(2017100130)
				.ComCpfCnpj("48340829033")
				.ComCursoId(this._curso.ID)
				.ComEndereco("Rua molina 423, Rio Comprido")
				.ComCelular("99999999")
				.ComDataNascimento(DateTime.Now.AddDays(-5000))
				.ComDataInicio(DateTime.Now.AddDays(-20))
				.ComDataFim(DateTime.Now.AddYears(4))
				.ComEmail("estevao.pulante2@unicarioca.com.br")
				.ComNome("Eduardo")
				.ComSobrenome("Pulante")
				.ComTelefone("2131593159")
				.ComId(Guid.NewGuid());

			this._aluno = Aluno.Criar(this._alunoDtoBuilder.Instanciar());

			this._tcc = Tcc.Criar("Inteligência Artificial", "descrição tema", new List<Guid> { this._professorBuilder.ObterProfessor().ID });

			var tccAlunoProfessor = TccAlunoProfessor.Criar(this._tcc.ID, this._professorBuilder.ObterProfessor().ID, this._alunoBuilder.ObterAluno().ID, "solicitacao");
			var tccAlunoProfessor2 = TccAlunoProfessor.Criar(this._tcc.ID, this._professorBuilder.ObterProfessor().ID, this._aluno.ID, "solicitacao");

			this._contextos.SmartContexto.Cursos.Add(this._curso);
			this._contextos.SmartContexto.Alunos.Add(this._aluno);
			this._contextos.SmartContexto.Tccs.Add(this._tcc);
			this._contextos.SmartContexto.TccAlunosProfessores.Add(tccAlunoProfessor);
			this._contextos.SmartContexto.TccAlunosProfessores.Add(tccAlunoProfessor2);
			this._contextos.SmartContexto.SaveChangesAsync();

			this._mediator = serviceProvider.GetRequiredService<IMediator>();
		}

		[Fact(DisplayName = "Lista solicitações de Tccs por Professor")]
		public async void DeveObterTccSolicitadoPorAluno()
		{
			var retornoSolicitacoesProfessor = await this._mediator.Send(new ListarTccsPorProfessorQuery
			{
				ProfessorId = this._professorBuilder.ObterProfessor().ID
			});

			var resultSolicitacoesObtidasPorProfessor = retornoSolicitacoesProfessor.Should().BeOfType<Result<IEnumerable<ObterSolicitacoesTccsDto>>>().Subject;

			resultSolicitacoesObtidasPorProfessor.Value.Should().NotBeNull();
			resultSolicitacoesObtidasPorProfessor.Value.Count().Should().Be(2);
			resultSolicitacoesObtidasPorProfessor.Value.Where(sp => sp.NomeAluno == this._aluno.Nome).Count().Should().Be(1);
			resultSolicitacoesObtidasPorProfessor.Value.Where(sp => sp.MatriculaAluno == this._aluno.Matricula).Count().Should().Be(1);
			resultSolicitacoesObtidasPorProfessor.Value.Where(sp => sp.Solicitacao == "solicitacao").Count().Should().Be(2);
			resultSolicitacoesObtidasPorProfessor.Value.Where(sp => sp.ProfessorID == this._professorBuilder.ObterProfessor().ID).Count().Should().Be(2);
			resultSolicitacoesObtidasPorProfessor.Value.Where(sp => sp.TccID == this._tcc.ID).Count().Should().Be(2);
			resultSolicitacoesObtidasPorProfessor.Value.Where(sp => sp.Tema == this._tcc.Tema).Count().Should().Be(2);
			resultSolicitacoesObtidasPorProfessor.Value.Where(sp => sp.AlunoID == this._aluno.ID).Count().Should().Be(1);
			resultSolicitacoesObtidasPorProfessor.Value.Where(sp => sp.DataSolicitacao.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy")).Count().Should().Be(2);
			resultSolicitacoesObtidasPorProfessor.Value.Where(sp => sp.NomeAluno == this._alunoBuilder.ObterAluno().Nome).Count().Should().Be(1);
			resultSolicitacoesObtidasPorProfessor.Value.Where(sp => sp.Status == "Solicitado").Count().Should().Be(2);
			resultSolicitacoesObtidasPorProfessor.Value.Where(sp => sp.EmailAluno == this._aluno.Email).Count().Should().Be(1);
		}
	}
}