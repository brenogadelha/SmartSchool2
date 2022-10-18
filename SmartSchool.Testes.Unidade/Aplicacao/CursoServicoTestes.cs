using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SmartSchool.Aplicacao.Cursos.Adicionar;
using SmartSchool.Aplicacao.Cursos.Alterar;
using SmartSchool.Aplicacao.Cursos.ObterPorId;
using SmartSchool.Aplicacao.Cursos.Remover;
using SmartSchool.Comum.Especificao;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Dominio.Cursos;
using SmartSchool.Dominio.Cursos.Servicos;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Disciplinas.Servicos;
using System;
using System.Collections.Generic;
using Xunit;

namespace SmartSchool.Testes.Unidade.Aplicacao
{
	public class CursoServicoTestes : TesteUnidade
	{
		private readonly IMediator _mediator;

		private readonly IDisciplinaServicoDominio _disciplinaServicoDominio;
		private readonly ICursoServicoDominio _cursoServicoDominio;

		private readonly Mock<IRepositorio<Curso>> _cursoRepositorioMock;
		private readonly Mock<IRepositorio<Disciplina>> _disciplinaRepositorioMock;

		public CursoServicoTestes()
		{
			this._disciplinaRepositorioMock = new Mock<IRepositorio<Disciplina>>();
			this._cursoRepositorioMock = new Mock<IRepositorio<Curso>>();

			this._disciplinaServicoDominio = new DisciplinaServicoDominio(this._disciplinaRepositorioMock.Object);
			this._cursoServicoDominio = new CursoServicoDominio(this._cursoRepositorioMock.Object);

			var serviceProvider = GetServiceProviderComMediatR((typeof(IRepositorio<Curso>), this._cursoRepositorioMock.Object), (typeof(IDisciplinaServicoDominio), this._disciplinaServicoDominio),
				(typeof(ICursoServicoDominio), this._cursoServicoDominio));

			this._mediator = serviceProvider.GetRequiredService<IMediator>();
		}

		[Fact(DisplayName = "Erro Ao Criar Curso - Já existe curso com o mesmo nome")]
		public void ErroAoCriarCurso_JaExisteMesmoNome()
		{
			var disciplinas = new List<Guid>();
			disciplinas.Add(Guid.NewGuid());

			var cursoDto = new AdicionarCursoCommand() { Nome = "Engenharia da Computação", DisciplinasId = disciplinas };

			this._cursoRepositorioMock.SetupSequence(x => x.ObterAsync(It.IsAny<IEspecificavel<Curso>>())).ReturnsAsync(new Curso());

			var exception = Assert.ThrowsAsync<ErroNegocioException>(() => this._mediator.Send(cursoDto));
			Assert.Equal($"Já existe um Curso com o mesmo nome '{cursoDto.Nome}'.", exception.Result.Message);

			this._cursoRepositorioMock.Verify(x => x.Adicionar(It.IsAny<Curso>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Alterar Curso - Id nulo ou inválido")]
		public void ErroAoAlterarCurso_IdNuloInvalido()
		{
			var disciplinas = new List<Guid>();
			disciplinas.Add(Guid.NewGuid());

			var cursoDto = new AlterarCursoCommand() { Nome = "Engenharia da Computação", DisciplinasId = disciplinas, ID = Guid.Empty };

			var exception = Assert.ThrowsAsync<ArgumentNullException>(() => this._mediator.Send(cursoDto));
			Assert.Equal("Id nulo do Curso (não foi informado).", exception.Result.Message);

			this._cursoRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Curso>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Alterar Curso - Curso não existe")]
		public void ErroAoAlterarCurso_NaoExiste()
		{
			var cursoId = Guid.NewGuid();
			var disciplinas = new List<Guid>();
			disciplinas.Add(Guid.NewGuid());

			var cursoDto = new AlterarCursoCommand() { Nome = "Engenharia da Computação", DisciplinasId = disciplinas, ID = cursoId };

			var exception = Assert.ThrowsAsync<RecursoInexistenteException>(() => this._mediator.Send(cursoDto));
			Assert.Equal($"Curso com ID '{cursoId}' não existe.", exception.Result.Message);

			this._cursoRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Curso>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Remover Curso - Id Nulo")]
		public void ErroAoExcluirCurso_IdNulo()
		{
			var exception = Assert.ThrowsAsync<ArgumentNullException>(() => this._mediator.Send(new RemoverCursoCommand { ID = Guid.Empty }));
			Assert.Equal("Id nulo do Curso (não foi informado).", exception.Result.Message);

			this._cursoRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Curso>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Remover Curso - Curso não existe")]
		public void ErroAoExcluirCurso_NaoExiste()
		{
			Guid id = Guid.NewGuid();
			var exception = Assert.ThrowsAsync<RecursoInexistenteException>(() => this._mediator.Send(new RemoverCursoCommand { ID = id }));
			Assert.Equal($"Curso com ID '{id}' não existe.", exception.Result.Message);

			this._cursoRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Curso>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Obter Curso - Por ID - Curso não existe")]
		public void ErroAoObterCurso_PorID_NaoExiste()
		{
			Guid id = Guid.NewGuid();
			var exception = Assert.ThrowsAsync<RecursoInexistenteException>(() => this._mediator.Send(new ObterCursoQuery { Id = id }));
			Assert.Equal($"Curso com ID '{id}' não existe.", exception.Result.Message);

			this._cursoRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Curso>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Obter Curso - Id Nulo")]
		public void ErroAoObterCurso_IdNulo()
		{
			var exception = Assert.ThrowsAsync<ArgumentNullException>(() => this._mediator.Send(new ObterCursoQuery { Id = Guid.Empty }));
			Assert.Equal("Id nulo do Curso (não foi informado).", exception.Result.Message);

			this._cursoRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Curso>(), It.IsAny<bool>()), Times.Never);
		}
	}
}