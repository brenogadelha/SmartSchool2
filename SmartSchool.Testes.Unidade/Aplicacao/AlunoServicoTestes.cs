using Moq;
using SmartSchool.Aplicacao.Alunos.Interface;
using SmartSchool.Aplicacao.Alunos.Servico;
using SmartSchool.Comum.Dominio.Enums;
using SmartSchool.Comum.Especificao;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Dominio.Alunos;
using SmartSchool.Dominio.Cursos;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Semestres;
using SmartSchool.Dto.Alunos;
using SmartSchool.Testes.Compartilhado.Builders;
using System;
using System.Collections.Generic;
using Xunit;

namespace SmartSchool.Testes.Unidade.Aplicacao
{
	public class AlunoServicoTestes : TesteUnidade
	{
		private readonly IAlunoServico _alunoServico;

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

			this._alunoServico = new AlunoServico(this._alunoRepositorioMock.Object, this._disciplinaRepositorioMock.Object, this._semestreRepositorioMock.Object, this._cursoRepositorioMock.Object);

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
			var aluno = this._alunoDtoBuilder.Instanciar();

			this._alunoRepositorioMock.SetupSequence(x => x.Obter(It.IsAny<IEspecificavel<Aluno>>())).Returns(new Aluno());

			var exception = Assert.Throws<ErroNegocioException>(() => this._alunoServico.CriarAluno(aluno));
			Assert.Equal($"Já existe um Aluno com o mesmo CPF '{aluno.Cpf}'.", exception.Message);

			this._alunoRepositorioMock.Verify(x => x.Adicionar(It.IsAny<Aluno>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Criar Aluno - Já Existe Aluno com este Email")]
		public void ErroAoCriarAluno_JaExisteMesmoEmail()
		{
			var aluno = this._alunoDtoBuilder.Instanciar();

			this._alunoRepositorioMock.SetupSequence(x => x.Obter(It.IsAny<IEspecificavel<Aluno>>())).Returns(null).Returns(new Aluno());

			var exception = Assert.Throws<ErroNegocioException>(() => this._alunoServico.CriarAluno(aluno));
			Assert.Equal($"Já existe um Aluno com o mesmo email '{aluno.Email}'.", exception.Message);

			this._alunoRepositorioMock.Verify(x => x.Adicionar(It.IsAny<Aluno>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Alterar Aluno - Id nulo ou inválido")]
		public void ErroAoAlterarAluno_IdUsuarioNuloInvalido()
		{
			var usuario = this._alunoDtoBuilder.ComId(Guid.Empty).InstanciarAlteracao();

			var exception = Assert.Throws<ArgumentNullException>(() => this._alunoServico.AlterarAluno(usuario.ID, usuario));
			Assert.Equal("Id nulo do Aluno (não foi informado).", exception.Message);

			this._alunoRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Aluno>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Alterar Aluno - Aluno não existe")]
		public void ErroAoAlterarAluno_UsuarioNaoExiste()
		{
			var usuario = this._alunoDtoBuilder.ComId(Guid.NewGuid()).InstanciarAlteracao();

			var exception = Assert.Throws<RecursoInexistenteException>(() => this._alunoServico.AlterarAluno(usuario.ID, usuario));
			Assert.Equal($"Aluno com ID '{usuario.ID}' não existe.", exception.Message);

			this._alunoRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Aluno>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Remover Aluno - Id Nulo")]
		public void ErroAoExcluirAluno_IdNulo()
		{
			var exception = Assert.Throws<ArgumentNullException>(() => this._alunoServico.Remover(Guid.Empty));
			Assert.Equal("Id nulo do Aluno (não foi informado).", exception.Message);

			this._alunoRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Aluno>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Remover Aluno - Aluno não existe")]
		public void ErroAoExcluirAluno_UsuarioNaoExiste()
		{
			Guid guid = Guid.NewGuid();
			var exception = Assert.Throws<RecursoInexistenteException>(() => this._alunoServico.Remover(guid));
			Assert.Equal($"Aluno com ID '{guid}' não existe.", exception.Message);

			this._alunoRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Aluno>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Obter Aluno - Por ID - Aluno não existe")]
		public void ErroAoObterAluno_PorID_NaoExiste()
		{
			Guid guid = Guid.NewGuid();
			var exception = Assert.Throws<RecursoInexistenteException>(() => this._alunoServico.ObterPorId(guid));
			Assert.Equal($"Aluno com ID '{guid}' não existe.", exception.Message);

			this._alunoRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Aluno>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Obter Aluno - Id Nulo")]
		public void ErroAoObterAluno_IdNulo()
		{
			var exception = Assert.Throws<ArgumentNullException>(() => this._alunoServico.ObterPorId(Guid.Empty));
			Assert.Equal("Id nulo do Aluno (não foi informado).", exception.Message);

			this._alunoRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Aluno>(), It.IsAny<bool>()), Times.Never);
		}
	}
}