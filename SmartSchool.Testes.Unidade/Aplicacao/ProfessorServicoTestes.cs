using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SmartSchool.Aplicacao.Professores.Adicionar;
using SmartSchool.Aplicacao.Professores.Alterar;
using SmartSchool.Aplicacao.Professores.ObterPorId;
using SmartSchool.Aplicacao.Professores.Remover;
using SmartSchool.Comum.Especificao;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Disciplinas.Servicos;
using SmartSchool.Dominio.Professores;
using SmartSchool.Dominio.Professores.Servicos;
using SmartSchool.Dto.Disciplinas;
using System;
using System.Collections.Generic;
using Xunit;

namespace SmartSchool.Testes.Unidade.Aplicacao
{
	public class ProfessorServicoTestes : TesteUnidade
	{
		private readonly Mock<IRepositorio<Professor>> _professorRepositorioMock;
		private readonly Mock<IRepositorio<Disciplina>> _disciplinaRepositorioMock;

		private readonly IMediator _mediator;

		private readonly DisciplinaDto _disciplinaDto1;
		private readonly DisciplinaDto _disciplinaDto2;
		private readonly DisciplinaDto _disciplinaDto3;

		private readonly Disciplina _disciplina1;
		private readonly Disciplina _disciplina2;
		private readonly Disciplina _disciplina3;

		public ProfessorServicoTestes()
		{
			this._disciplinaRepositorioMock = new Mock<IRepositorio<Disciplina>>();
			this._professorRepositorioMock = new Mock<IRepositorio<Professor>>();

			var disciplinaServicoDominio = new DisciplinaServicoDominio(this._disciplinaRepositorioMock.Object);
			var professorServicoDominio = new ProfessorServicoDominio(this._professorRepositorioMock.Object);

			var serviceProvider = GetServiceProviderComMediatR((typeof(IRepositorio<Professor>), this._professorRepositorioMock.Object),
				(typeof(IDisciplinaServicoDominio), disciplinaServicoDominio), (typeof(IProfessorServicoDominio), professorServicoDominio));

			this._mediator = serviceProvider.GetRequiredService<IMediator>();

			// Criação de Disciplinas
			this._disciplinaDto1 = new DisciplinaDto() { Nome = "Linguagens Formais e Automatos", Periodo = 1 };
			this._disciplinaDto2 = new DisciplinaDto() { Nome = "Teoria em Grafos", Periodo = 2 };
			this._disciplinaDto3 = new DisciplinaDto() { Nome = "Projeto Integrador", Periodo = 3 };

			this._disciplina1 = Disciplina.Criar(_disciplinaDto1);
			this._disciplina2 = Disciplina.Criar(_disciplinaDto2);
			this._disciplina3 = Disciplina.Criar(_disciplinaDto3);
		}

		[Fact(DisplayName = "Erro Ao Criar Professor - Já Existe Professor com esta Matricula")]
		public void ErroAoCriarProfessor_JaExisteMesmaMatricula()
		{
			var disciplinas = new List<Guid>();
			disciplinas.Add(Guid.NewGuid());

			var professorDto = new AdicionarProfessorCommand() { Matricula = 2017100150, Nome = "Paulo Roberto", Disciplinas = disciplinas };

			this._professorRepositorioMock.SetupSequence(x => x.ObterAsync(It.IsAny<IEspecificavel<Professor>>())).ReturnsAsync(new Professor());

			var exception = Assert.ThrowsAsync<ErroNegocioException>(() => this._mediator.Send(professorDto));
			Assert.Equal($"Já existe um Professor com a mesma matricula '{professorDto.Matricula}'.", exception.Result.Message);

			this._professorRepositorioMock.Verify(x => x.Adicionar(It.IsAny<Professor>(), It.IsAny<bool>()), Times.Never);
		}


		[Fact(DisplayName = "Erro Ao Alterar Professor - Id nulo ou inválido")]
		public void ErroAoAlterarProfessor_IdProfessorNuloInvalido()
		{
			var professorDto = new AlterarProfessorCommand() { Matricula = 2017100150, Nome = "Paulo Roberto", Disciplinas = new List<Guid>() { _disciplina1.ID, _disciplina2.ID, _disciplina3.ID }, ID = Guid.Empty };

			var exception = Assert.ThrowsAsync<ArgumentNullException>(() => this._mediator.Send(professorDto));
			Assert.Equal("Id nulo do Professor (não foi informado).", exception.Result.Message);

			this._professorRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Professor>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Alterar Professor - Professor não existe")]
		public void ErroAoAlterarProfessor_ProfessorNaoExiste()
		{
			var professorId = Guid.NewGuid();
			var professorDto = new AlterarProfessorCommand() { Matricula = 2017100150, Nome = "Paulo Roberto", Disciplinas = new List<Guid>() { _disciplina1.ID, _disciplina2.ID, _disciplina3.ID }, ID = professorId };

			var exception = Assert.ThrowsAsync<RecursoInexistenteException>(() => this._mediator.Send(professorDto));
			Assert.Equal($"Professor com ID '{professorId}' não existe.", exception.Result.Message);

			this._professorRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Professor>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Remover Professor - Id Nulo")]
		public void ErroAoExcluirProfessor_IdNulo()
		{
			var exception = Assert.ThrowsAsync<ArgumentNullException>(() => this._mediator.Send(new RemoverProfessorCommand { ID = Guid.Empty }));
			Assert.Equal("Id nulo do Professor (não foi informado).", exception.Result.Message);

			this._professorRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Professor>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Remover Professor - Professor não existe")]
		public void ErroAoExcluirProfessor_ProfessorNaoExiste()
		{
			Guid id = Guid.NewGuid();
			var exception = Assert.ThrowsAsync<RecursoInexistenteException>(() => this._mediator.Send(new RemoverProfessorCommand { ID = id }));
			Assert.Equal($"Professor com ID '{id}' não existe.", exception.Result.Message);

			this._professorRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Professor>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Obter Professor - Por ID - Professor não existe")]
		public void ErroAoObterProfessor_PorID_ProfessorNaoExiste()
		{
			Guid id = Guid.NewGuid();
			var exception = Assert.ThrowsAsync<RecursoInexistenteException>(() => this._mediator.Send(new ObterProfessorQuery { Id = id }));
			Assert.Equal($"Professor com ID '{id}' não existe.", exception.Result.Message);

			this._professorRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Professor>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Obter Professor - Id Nulo")]
		public void ErroAoObterProfessor_IdNulo()
		{
			var exception = Assert.ThrowsAsync<ArgumentNullException>(() => this._mediator.Send(new ObterProfessorQuery { Id = Guid.Empty }));
			Assert.Equal("Id nulo do Professor (não foi informado).", exception.Result.Message);

			this._professorRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Professor>(), It.IsAny<bool>()), Times.Never);
		}
	}
}