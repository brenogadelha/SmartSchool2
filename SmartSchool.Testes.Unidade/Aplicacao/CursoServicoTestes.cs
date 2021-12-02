using Moq;
using SmartSchool.Aplicacao.Cursos.Interface;
using SmartSchool.Aplicacao.Cursos.Servico;
using SmartSchool.Comum.Especificao;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Dominio.Cursos;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dto.Curso;
using System;
using System.Collections.Generic;
using Xunit;

namespace SmartSchool.Testes.Unidade.Aplicacao
{
	public class CursoServicoTestes : TesteUnidade
	{
		private readonly ICursoServico _cursoServico;

		private readonly Mock<IRepositorio<Curso>> _cursoRepositorioMock;
		private readonly Mock<IRepositorio<Disciplina>> _disciplinaRepositorioMock;

		public CursoServicoTestes()
		{
			this._cursoRepositorioMock = new Mock<IRepositorio<Curso>>();
			this._disciplinaRepositorioMock = new Mock<IRepositorio<Disciplina>>();

			this._cursoServico = new CursoServico(this._cursoRepositorioMock.Object, this._disciplinaRepositorioMock.Object);
		}

		[Fact(DisplayName = "Erro Ao Criar Curso - Já existe curso com o mesmo nome")]
		public void ErroAoCriarCurso_JaExisteMesmoNome()
		{
			var disciplinas = new List<Guid>();
			disciplinas.Add(Guid.NewGuid());

			var cursoDto = new AlterarCursoDto() { Nome = "Engenharia da Computação", DisciplinasId = disciplinas };

			this._cursoRepositorioMock.SetupSequence(x => x.Obter(It.IsAny<IEspecificavel<Curso>>())).Returns(new Curso());

			var exception = Assert.Throws<ErroNegocioException>(() => this._cursoServico.CriarCurso(cursoDto));
			Assert.Equal($"Já existe um Curso com o mesmo nome '{cursoDto.Nome}'.", exception.Message);

			this._cursoRepositorioMock.Verify(x => x.Adicionar(It.IsAny<Curso>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Alterar Curso - Id nulo ou inválido")]
		public void ErroAoAlterarCurso_IdNuloInvalido()
		{
			var disciplinas = new List<Guid>();
			disciplinas.Add(Guid.NewGuid());

			var cursoDto = new AlterarCursoDto() { Nome = "Engenharia da Computação", DisciplinasId = disciplinas };

			var exception = Assert.Throws<ArgumentNullException>(() => this._cursoServico.AlterarCurso(Guid.Empty, cursoDto));
			Assert.Equal("Id nulo do Curso (não foi informado).", exception.Message);

			this._cursoRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Curso>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Alterar Curso - Curso não existe")]
		public void ErroAoAlterarCurso_NaoExiste()
		{
			var cursoId = Guid.NewGuid();
			var disciplinas = new List<Guid>();
			disciplinas.Add(Guid.NewGuid());

			var cursoDto = new AlterarCursoDto() { Nome = "Engenharia da Computação", DisciplinasId = disciplinas };

			var exception = Assert.Throws<RecursoInexistenteException>(() => this._cursoServico.AlterarCurso(cursoId, cursoDto));
			Assert.Equal($"Curso com ID '{cursoId}' não existe.", exception.Message);

			this._cursoRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Curso>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Remover Curso - Id Nulo")]
		public void ErroAoExcluirCurso_IdNulo()
		{
			var exception = Assert.Throws<ArgumentNullException>(() => this._cursoServico.Remover(Guid.Empty));
			Assert.Equal("Id nulo do Curso (não foi informado).", exception.Message);

			this._cursoRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Curso>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Remover Curso - Curso não existe")]
		public void ErroAoExcluirCurso_NaoExiste()
		{
			Guid guid = Guid.NewGuid();
			var exception = Assert.Throws<RecursoInexistenteException>(() => this._cursoServico.Remover(guid));
			Assert.Equal($"Curso com ID '{guid}' não existe.", exception.Message);

			this._cursoRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Curso>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Obter Curso - Por ID - Curso não existe")]
		public void ErroAoObterCurso_PorID_NaoExiste()
		{
			Guid guid = Guid.NewGuid();
			var exception = Assert.Throws<RecursoInexistenteException>(() => this._cursoServico.ObterPorId(guid));
			Assert.Equal($"Curso com ID '{guid}' não existe.", exception.Message);

			this._cursoRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Curso>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Obter Curso - Id Nulo")]
		public void ErroAoObterCurso_IdNulo()
		{
			var exception = Assert.Throws<ArgumentNullException>(() => this._cursoServico.ObterPorId(Guid.Empty));
			Assert.Equal("Id nulo do Curso (não foi informado).", exception.Message);

			this._cursoRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Curso>(), It.IsAny<bool>()), Times.Never);
		}
	}
}