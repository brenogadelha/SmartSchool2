using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SmartSchool.Aplicacao.Disciplinas.Adicionar;
using SmartSchool.Aplicacao.Disciplinas.Alterar;
using SmartSchool.Aplicacao.Disciplinas.ObterPorId;
using SmartSchool.Aplicacao.Disciplinas.Remover;
using SmartSchool.Comum.Especificao;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Disciplinas.Servicos;
using SmartSchool.Dto.Curso;
using System;
using Xunit;

namespace SmartSchool.Testes.Unidade.Aplicacao
{
	public class DisciplinaServicoTestes : TesteUnidade
	{
		private readonly IMediator _mediator;

		private readonly IDisciplinaServicoDominio _disciplinaServicoDominio;

		private readonly Mock<IRepositorio<Disciplina>> _disciplinaRepositorioMock;

		public DisciplinaServicoTestes()
		{
			this._disciplinaRepositorioMock = new Mock<IRepositorio<Disciplina>>();

			this._disciplinaServicoDominio = new DisciplinaServicoDominio(this._disciplinaRepositorioMock.Object);

			var serviceProvider = GetServiceProviderComMediatR((typeof(IRepositorio<Disciplina>), this._disciplinaRepositorioMock.Object), (typeof(IDisciplinaServicoDominio), this._disciplinaServicoDominio));

			this._mediator = serviceProvider.GetRequiredService<IMediator>();
		}

		[Fact(DisplayName = "Erro Ao Criar Disciplina - Já existe Disciplina com o mesmo nome")]
		public void ErroAoCriarDisciplina_JaExisteMesmoNome()
		{
			var disciplinaDto = new AdicionarDisciplinaCommand() { Nome = "Linguagens Formais e Automatos", Periodo = 1 };

			this._disciplinaRepositorioMock.SetupSequence(x => x.ObterAsync(It.IsAny<IEspecificavel<Disciplina>>())).ReturnsAsync(new Disciplina());

			var retorno = this._mediator.Send(disciplinaDto);

			retorno.Should().NotBeNull();
			retorno.Result.Status.Should().Be(Result.UnprocessableEntity().Status);
			retorno.Result.Errors.Should().AllBe($"Já existe uma Disciplina com o mesmo nome '{disciplinaDto.Nome}'.");

			this._disciplinaRepositorioMock.Verify(x => x.Adicionar(It.IsAny<Disciplina>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Alterar Disciplina - Já existe Disciplina com o mesmo nome")]
		public void ErroAoAlterarDisciplina_JaExisteMesmoNome()
		{
			var disciplinaDto = new AlterarDisciplinaCommand() { Nome = "Linguagens Formais e Automatos", Periodo = 1, ID = Guid.NewGuid() };

			this._disciplinaRepositorioMock.SetupSequence(x => x.ObterAsync(It.IsAny<IEspecificavel<Disciplina>>())).ReturnsAsync(new Disciplina());

			var retorno = this._mediator.Send(disciplinaDto);

			retorno.Should().NotBeNull();
			retorno.Result.Status.Should().Be(Result.UnprocessableEntity().Status);
			retorno.Result.Errors.Should().AllBe($"Já existe uma Disciplina com o mesmo nome '{disciplinaDto.Nome}'.");

			this._disciplinaRepositorioMock.Verify(x => x.Adicionar(It.IsAny<Disciplina>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Alterar Disciplina - Id nulo ou inválido")]
		public void ErroAoAlterarDisciplina_IdNuloInvalido()
		{
			var disciplinaDto = new AlterarDisciplinaCommand() { Nome = "Linguagens Formais e Automatos", Periodo = 1, ID = Guid.Empty };

			var exception = Assert.ThrowsAsync<ArgumentNullException>(() => this._mediator.Send(disciplinaDto));
			Assert.Equal("Id nulo da Disciplina (não foi informado).", exception.Result.Message);

			this._disciplinaRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Disciplina>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Alterar Disciplina - Disciplina não existe")]
		public void ErroAoAlterarDisciplina_NaoExiste()
		{
			var disciplinaId = Guid.NewGuid();
			var disciplinaDto = new AlterarDisciplinaCommand() { Nome = "Linguagens Formais e Automatos", Periodo = 1, ID = disciplinaId };

			var exception = Assert.ThrowsAsync<RecursoInexistenteException>(() => this._mediator.Send(disciplinaDto));
			Assert.Equal($"Disciplina com ID '{disciplinaId}' não existe.", exception.Result.Message);

			this._disciplinaRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Disciplina>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Remover Disciplina - Id Nulo")]
		public void ErroAoExcluirDisciplina_IdNulo()
		{
			var exception = Assert.ThrowsAsync<ArgumentNullException>(() => this._mediator.Send(new RemoverDisciplinaCommand { ID = Guid.Empty }));
			Assert.Equal("Id nulo da Disciplina (não foi informado).", exception.Result.Message);

			this._disciplinaRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Disciplina>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Remover Disciplina - Disciplina não existe")]
		public void ErroAoExcluirDisciplina_NaoExiste()
		{
			Guid id = Guid.NewGuid();
			var exception = Assert.ThrowsAsync<RecursoInexistenteException>(() => this._mediator.Send(new RemoverDisciplinaCommand { ID = id }));
			Assert.Equal($"Disciplina com ID '{id}' não existe.", exception.Result.Message);

			this._disciplinaRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Disciplina>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Obter Disciplina - Por ID - Disciplina não existe")]
		public void ErroAoObterDisciplina_PorID_NaoExiste()
		{
			Guid id = Guid.NewGuid();
			var exception = Assert.ThrowsAsync<RecursoInexistenteException>(() => this._mediator.Send(new ObterDisciplinaQuery { Id = id }));
			Assert.Equal($"Disciplina com ID '{id}' não existe.", exception.Result.Message);

			this._disciplinaRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Disciplina>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Obter Disciplina - Id Nulo")]
		public void ErroAoObterDisciplina_IdNulo()
		{
			var exception = Assert.ThrowsAsync<ArgumentNullException>(() => this._mediator.Send(new ObterDisciplinaQuery { Id = Guid.Empty }));
			Assert.Equal("Id nulo da Disciplina (não foi informado).", exception.Result.Message);

			this._disciplinaRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Disciplina>(), It.IsAny<bool>()), Times.Never);
		}
	}
}