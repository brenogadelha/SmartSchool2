using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SmartSchool.Aplicacao.Tccs.Adicionar;
using SmartSchool.Aplicacao.Tccs.Alterar;
using SmartSchool.Aplicacao.Tccs.ObterPorAluno;
using SmartSchool.Aplicacao.Tccs.ObterPorId;
using SmartSchool.Aplicacao.Tccs.Remover;
using SmartSchool.Comum.Especificao;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Dados.Modulos.Alunos;
using SmartSchool.Dominio.Alunos.Servicos;
using SmartSchool.Dominio.Alunos;
using SmartSchool.Dominio.Professores;
using SmartSchool.Dominio.Professores.Servicos;
using SmartSchool.Dominio.Tccs;
using SmartSchool.Dominio.Tccs.Servicos;
using System;
using System.Collections.Generic;
using Xunit;
using SmartSchool.Aplicacao.Tccs.Solicitar;
using SmartSchool.Aplicacao.Tccs.Aprovar;
using SmartSchool.Comum.Dominio.Enums;

namespace SmartSchool.Testes.Unidade.Aplicacao
{
	public class TccServicoTestes : TesteUnidade
	{
		private readonly IMediator _mediator;

		private readonly IProfessorServicoDominio _professorServicoDominio;
		private readonly ITccServicoDominio _tccServicoDominio;
		private readonly IAlunoServicoDominio _alunoServicoDominio;

		private readonly Mock<IRepositorio<Tcc>> _tccRepositorioMock;
		private readonly Mock<IRepositorio<Professor>> _professorRepositorioMock;
		private readonly Mock<IRepositorio<Aluno>> _alunoRepositorioMock;
		private readonly Mock<IRepositorio<TccAlunoProfessor>> _tccAlunoProfessorRepositorioMock;

		public TccServicoTestes()
		{
			this._professorRepositorioMock = new Mock<IRepositorio<Professor>>();
			this._tccRepositorioMock = new Mock<IRepositorio<Tcc>>();
			this._alunoRepositorioMock = new Mock<IRepositorio<Aluno>>();
			this._tccAlunoProfessorRepositorioMock = new Mock<IRepositorio<TccAlunoProfessor>>();

			this._professorServicoDominio = new ProfessorServicoDominio(this._professorRepositorioMock.Object);
			this._tccServicoDominio = new TccServicoDominio(this._tccRepositorioMock.Object);
			this._alunoServicoDominio = new AlunoServicoDominio(this._alunoRepositorioMock.Object);

			var serviceProvider = GetServiceProviderComMediatR((typeof(IRepositorio<Tcc>), this._tccRepositorioMock.Object), (typeof(IRepositorio<Aluno>), this._alunoRepositorioMock.Object),
				(typeof(IRepositorio<TccAlunoProfessor>), this._tccAlunoProfessorRepositorioMock.Object), (typeof(IProfessorServicoDominio), this._professorServicoDominio),
				(typeof(ITccServicoDominio), this._tccServicoDominio), (typeof(IAlunoServicoDominio), this._alunoServicoDominio));

			this._mediator = serviceProvider.GetRequiredService<IMediator>();
		}

		[Fact(DisplayName = "Erro Ao Criar Tcc - Já existe Tcc com o mesmo tema")]
		public void ErroAoCriarTcc_JaExisteMesmoTema()
		{
			var professores = new List<Guid>();
			professores.Add(Guid.NewGuid());

			var tccCommand = new AdicionarTccCommand() { Tema = "Visualização de Dados e Automação", Descricao = "descrição tema", Professores = professores };

			this._tccRepositorioMock.SetupSequence(x => x.ObterAsync(It.IsAny<IEspecificavel<Tcc>>())).ReturnsAsync(new Tcc());

			var exception = Assert.ThrowsAsync<ErroNegocioException>(() => this._mediator.Send(tccCommand));
			Assert.Equal($"Já existe um Tcc com o mesmo Tema '{tccCommand.Tema}'.", exception.Result.Message);

			this._tccRepositorioMock.Verify(x => x.Adicionar(It.IsAny<Tcc>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Alterar Tcc - Id nulo ou inválido")]
		public void ErroAoAlterarTcc_IdNuloInvalido()
		{
			var professores = new List<Guid>();
			professores.Add(Guid.NewGuid());

			var tccCommand = new AlterarTccCommand() { Tema = "Visualização de Dados e Automação", Descricao = "descrição tema", Professores = professores };

			this._professorRepositorioMock.SetupSequence(x => x.ObterAsync(It.IsAny<IEspecificavel<Professor>>())).ReturnsAsync(new Professor());

			var exception = Assert.ThrowsAsync<ArgumentNullException>(() => this._mediator.Send(tccCommand));
			Assert.Equal("Id nulo do Tcc (não foi informado).", exception.Result.Message);

			this._tccRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Tcc>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Alterar Tcc - Já existe Tcc com o mesmo tema")]
		public void ErroAoAlterarTcc_JaExisteMesmoTema()
		{
			var tccId = Guid.NewGuid();

			var professores = new List<Guid>();
			professores.Add(Guid.NewGuid());

			var tccCommand = new AlterarTccCommand() { Tema = "Visualização de Dados e Automação", Descricao = "descrição tema", Professores = professores };

			this._tccRepositorioMock.SetupSequence(x => x.ObterAsync(It.IsAny<IEspecificavel<Tcc>>())).ReturnsAsync(new Tcc());

			var exception = Assert.ThrowsAsync<ErroNegocioException>(() => this._mediator.Send(tccCommand));
			Assert.Equal($"Já existe um Tcc com o mesmo Tema '{tccCommand.Tema}'.", exception.Result.Message);

			this._tccRepositorioMock.Verify(x => x.Adicionar(It.IsAny<Tcc>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Alterar Tcc - Tcc não existe")]
		public void ErroAoAlterarTcc_NaoExiste()
		{
			var tccId = Guid.NewGuid();

			var professores = new List<Guid>();
			professores.Add(Guid.NewGuid());

			var tccCommand = new AlterarTccCommand() { Tema = "Visualização de Dados e Automação", Descricao = "descrição tema", Professores = professores, ID = tccId };

			this._professorRepositorioMock.SetupSequence(x => x.ObterAsync(It.IsAny<IEspecificavel<Professor>>())).ReturnsAsync(new Professor());

			var exception = Assert.ThrowsAsync<RecursoInexistenteException>(() => this._mediator.Send(tccCommand));
			Assert.Equal($"Tcc com ID '{tccId}' não existe.", exception.Result.Message);

			this._tccRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Tcc>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Remover Tcc - Id Nulo")]
		public void ErroAoExcluirTcc_IdNulo()
		{
			var exception = Assert.ThrowsAsync<ArgumentNullException>(() => this._mediator.Send(new RemoverTccCommand { ID = Guid.Empty }));
			Assert.Equal("Id nulo do Tcc (não foi informado).", exception.Result.Message);

			this._tccRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Tcc>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Remover Tcc - Tcc não existe")]
		public void ErroAoExcluirTcc_NaoExiste()
		{
			Guid id = Guid.NewGuid();
			var exception = Assert.ThrowsAsync<RecursoInexistenteException>(() => this._mediator.Send(new RemoverTccCommand { ID = id }));
			Assert.Equal($"Tcc com ID '{id}' não existe.", exception.Result.Message);

			this._tccRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Tcc>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Obter Tcc - Por ID - Tcc não existe")]
		public void ErroAoObterTcc_PorID_NaoExiste()
		{
			Guid id = Guid.NewGuid();
			var exception = Assert.ThrowsAsync<RecursoInexistenteException>(() => this._mediator.Send(new ObterTccQuery { Id = id }));
			Assert.Equal($"Tcc com ID '{id}' não existe.", exception.Result.Message);

			this._tccRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Tcc>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Obter Tcc - Id Nulo")]
		public void ErroAoObterTcc_IdNulo()
		{
			var exception = Assert.ThrowsAsync<ArgumentNullException>(() => this._mediator.Send(new ObterTccQuery { Id = Guid.Empty }));
			Assert.Equal("Id nulo do Tcc (não foi informado).", exception.Result.Message);

			this._tccRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Tcc>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Obter solicitação de Tcc por Aluno - Não existe")]
		public void ErroAoObterSolicitacaoTccPorAluno()
		{
			var exception = Assert.ThrowsAsync<RecursoInexistenteException>(() => this._mediator.Send(new ObterTccPorAlunoQuery { AlunoId = Guid.Empty }));
			Assert.Equal("Não existe solicitação de TCC para o aluno informado.", exception.Result.Message);

			this._tccRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Tcc>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao solicitar Tcc - Grupo maior que 3 alunos")]
		public void ErroAoSolicitarTcc_GrupoMaiorQueTres()
		{
			this._tccRepositorioMock.SetupSequence(x => x.ObterAsync(It.IsAny<IEspecificavel<Tcc>>())).ReturnsAsync(new Tcc());

			this._professorRepositorioMock.SetupSequence(x => x.ObterAsync(It.IsAny<IEspecificavel<Professor>>())).ReturnsAsync(new Professor());

			var exception = Assert.ThrowsAsync<ErroNegocioException>(() => this._mediator.Send(new SolicitarTccCommand { AlunosIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() }, TccId = Guid.NewGuid(), ProfessorId = Guid.NewGuid() }));
			Assert.Equal("O grupo para o TCC deve ser formado por no máximo 3 alunos.", exception.Result.Message);

			this._tccRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Tcc>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Aprovar Tcc - Solicitação não encontrada")]
		public void ErroAoAprovarTcc_NaoEncontrado()
		{
			//this._tccAlunoProfessorRepositorioMock.SetupSequence(x => x.ObterAsync(It.IsAny<IEspecificavel<TccAlunoProfessor>>())).ReturnsAsync(new TccAlunoProfessor());

			var exception = Assert.ThrowsAsync<RecursoInexistenteException>(() => this._mediator.Send(new AprovarTccCommand { ProfessorId = Guid.NewGuid(), AlunoId = Guid.NewGuid(), StatusTcc = TccStatus.Aceito }));
			Assert.Equal("Não foi encontrada solicitação de TCC para o Aluno informado.", exception.Result.Message);

			this._tccRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Tcc>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Aprovar Tcc - Resposta não informada ao Negar")]
		public void ErroAoAprovarTcc_SemRespostaAoNegar()
		{
			this._tccAlunoProfessorRepositorioMock.SetupSequence(x => x.ObterAsync(It.IsAny<IEspecificavel<TccAlunoProfessor>>())).ReturnsAsync(new TccAlunoProfessor());

			var exception = Assert.ThrowsAsync<ErroNegocioException>(() => this._mediator.Send(new AprovarTccCommand { ProfessorId = Guid.NewGuid(), AlunoId = Guid.NewGuid(), StatusTcc = TccStatus.Negado }));
			Assert.Equal("Em caso de negação, é necessário informar o motivo.", exception.Result.Message);

			this._tccRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Tcc>(), It.IsAny<bool>()), Times.Never);
		}
	}
}