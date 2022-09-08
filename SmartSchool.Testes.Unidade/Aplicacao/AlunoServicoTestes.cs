using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SmartSchool.Aplicacao.Alunos.ObterPorId;
using SmartSchool.Aplicacao.Alunos.RemoverAluno;
using SmartSchool.Comum.Dominio.Enums;
using SmartSchool.Comum.Especificao;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Dominio.Alunos;
using SmartSchool.Dominio.Alunos.Servicos;
using SmartSchool.Dominio.Cursos;
using SmartSchool.Dominio.Cursos.Servicos;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Disciplinas.Servicos;
using SmartSchool.Dominio.Semestres;
using SmartSchool.Dominio.Semestres.Servicos;
using SmartSchool.Dto.Alunos;
using SmartSchool.Testes.Compartilhado.Builders;
using System;
using System.Collections.Generic;
using Xunit;

namespace SmartSchool.Testes.Unidade.Aplicacao
{
	public class AlunoServicoTestes : TesteUnidade
	{
		private readonly IMediator _mediator;
		private readonly IDisciplinaServicoDominio _disciplinaServicoDominio;
		private readonly ISemestreServicoDominio _semestreServicoDominio;
		private readonly ICursoServicoDominio _cursoServicoDominio;
		private readonly IAlunoServicoDominio _alunoServicoDominio;

		private readonly Mock<IRepositorio<Aluno>> _alunoRepositorioMock;
		private readonly Mock<IRepositorio<Disciplina>> _disciplinaRepositorioMock;
		private readonly Mock<IRepositorio<Semestre>> _semestreRepositorioMock;
		private readonly Mock<IRepositorio<Curso>> _cursoRepositorioMock;

		private readonly AlunoDtoBuilder _alunoDtoBuilder;

		public AlunoServicoTestes()
		{
			this._alunoRepositorioMock = new Mock<IRepositorio<Aluno>>();
			this._disciplinaRepositorioMock = new Mock<IRepositorio<Disciplina>>();
			this._semestreRepositorioMock = new Mock<IRepositorio<Semestre>>();
			this._cursoRepositorioMock = new Mock<IRepositorio<Curso>>();

			this._disciplinaServicoDominio = new DisciplinaServicoDominio(this._disciplinaRepositorioMock.Object);
			this._semestreServicoDominio = new SemestreServicoDominio(this._semestreRepositorioMock.Object);
			this._cursoServicoDominio = new CursoServicoDominio(this._cursoRepositorioMock.Object);
			this._alunoServicoDominio = new AlunoServicoDominio(this._alunoRepositorioMock.Object);

			var serviceProvider = GetServiceProviderComMediatR((typeof(IRepositorio<Aluno>), this._alunoRepositorioMock.Object), (typeof(IDisciplinaServicoDominio), this._disciplinaServicoDominio),
			(typeof(ISemestreServicoDominio), this._semestreServicoDominio), (typeof(ICursoServicoDominio), this._cursoServicoDominio), (typeof(IAlunoServicoDominio), this._alunoServicoDominio));

			this._mediator = serviceProvider.GetRequiredService<IMediator>();

			var alunoDisciplinaDto = new AlunoDisciplinaDto()
			{
				DisciplinaId = Guid.NewGuid(),
				Periodo = 1,
				SemestreId = Guid.NewGuid(),
				StatusDisciplina = StatusDisciplina.Cursando
			};

			List<AlunoDisciplinaDto> alunosDisciplinas = new List<AlunoDisciplinaDto>();
			alunosDisciplinas.Add(alunoDisciplinaDto);

			this._alunoDtoBuilder = AlunoDtoBuilder.Novo
				.ComCidade("Rio de Janeiro")
				.ComCpfCnpj("48340829033")
				.ComCelular("99999999")
				.ComAlunosDisciplinas(alunosDisciplinas)
				.ComDataNascimento(DateTime.Now.AddDays(-5000))
				.ComDataInicio(DateTime.Now)
				.ComEmail("estevao.pulante@unicarioca.com.br")
				.ComNome("Estevão")
				.ComSobrenome("Pulante")
				.ComTelefone("2131593159")
				.ComId(Guid.NewGuid());
		}

		[Fact(DisplayName = "Erro Ao Criar Aluno - Já Existe Aluno com este Cpf")]
		public void ErroAoCriarAluno_JaExisteMesmoCpfCnpj()
		{
			var aluno = this._alunoDtoBuilder.InstanciarCommand();

			this._alunoRepositorioMock.SetupSequence(x => x.ObterAsync(It.IsAny<IEspecificavel<Aluno>>())).ReturnsAsync(new Aluno());

			var exception = Assert.ThrowsAsync<ErroNegocioException>(() => this._mediator.Send(aluno));
			Assert.Equal($"Já existe um Aluno com o mesmo CPF '{aluno.Cpf}'.", exception.Result.Message);

			this._alunoRepositorioMock.Verify(x => x.Adicionar(It.IsAny<Aluno>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Criar Aluno - Já Existe Aluno com este Email")]
		public void ErroAoCriarAluno_JaExisteMesmoEmail()
		{
			var aluno = this._alunoDtoBuilder.InstanciarCommand();

			this._alunoRepositorioMock.SetupSequence(x => x.ObterAsync(It.IsAny<IEspecificavel<Aluno>>())).ReturnsAsync(null).ReturnsAsync(new Aluno());

			var exception = Assert.ThrowsAsync<ErroNegocioException>(() => this._mediator.Send(aluno));
			Assert.Equal($"Já existe um Aluno com o mesmo email '{aluno.Email}'.", exception.Result.Message);

			this._alunoRepositorioMock.Verify(x => x.Adicionar(It.IsAny<Aluno>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Alterar Aluno - Id nulo ou inválido")]
		public void ErroAoAlterarAluno_IdUsuarioNuloInvalido()
		{
			var aluno = this._alunoDtoBuilder.ComId(Guid.Empty).InstanciarCommandAlteracao();

			var exception = Assert.ThrowsAsync<ArgumentNullException>(() => this._mediator.Send(aluno));
			Assert.Equal("Id nulo do Aluno (não foi informado).", exception.Result.Message);

			this._alunoRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Aluno>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Alterar Aluno - Aluno não existe")]
		public void ErroAoAlterarAluno_AlunoNaoExiste()
		{
			var aluno = this._alunoDtoBuilder.ComId(Guid.NewGuid()).InstanciarCommandAlteracao();

			var exception = Assert.ThrowsAsync<RecursoInexistenteException>(() => this._mediator.Send(aluno));
			Assert.Equal($"Aluno com ID '{aluno.ID}' não existe.", exception.Result.Message);

			this._alunoRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Aluno>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Remover Aluno - Id Nulo")]
		public void ErroAoExcluirAluno_IdNulo()
		{
			var exception = Assert.ThrowsAsync<ArgumentNullException>(() => this._mediator.Send(new RemoverAlunoCommand { ID = Guid.Empty }));
			Assert.Equal("Id nulo do Aluno (não foi informado).", exception.Result.Message);

			this._alunoRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Aluno>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Remover Aluno - Aluno não existe")]
		public void ErroAoExcluirAluno_AlunoNaoExiste()
		{
			Guid id = Guid.NewGuid();

			var exception = Assert.ThrowsAsync<RecursoInexistenteException>(() => this._mediator.Send(new RemoverAlunoCommand { ID = id }));
			Assert.Equal($"Aluno com ID '{id}' não existe.", exception.Result.Message);

			this._alunoRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Aluno>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Obter Aluno - Por ID - Aluno não existe")]
		public void ErroAoObterAluno_PorID_NaoExiste()
		{
			Guid id = Guid.NewGuid();
			var exception = Assert.ThrowsAsync<RecursoInexistenteException>(() => this._mediator.Send(new ObterAlunoCommand { Id = id }));
			Assert.Equal($"Aluno com ID '{id}' não existe.", exception.Result.Message);

			this._alunoRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Aluno>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Obter Aluno - Id Nulo")]
		public void ErroAoObterAluno_IdNulo()
		{
			var exception = Assert.ThrowsAsync<ArgumentNullException>(() => this._mediator.Send(new ObterAlunoCommand { Id = Guid.Empty }));
			Assert.Equal("Id nulo do Aluno (não foi informado).", exception.Result.Message);

			this._alunoRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Aluno>(), It.IsAny<bool>()), Times.Never);
		}
	}
}