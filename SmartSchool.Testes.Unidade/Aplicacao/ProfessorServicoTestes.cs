using Moq;
using SmartSchool.Aplicacao.Professores.Interface;
using SmartSchool.Aplicacao.Professores.Servico;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Professores;
using SmartSchool.Dto.Disciplinas;
using SmartSchool.Dto.Dtos.Professores;
using System;
using System.Collections.Generic;
using Xunit;

namespace SmartSchool.Testes.Unidade.Aplicacao
{
	public class ProfessorServicoTestes : TesteUnidade
	{
		private readonly IProfessorServico _professorServico;

		private readonly Mock<IRepositorio<Professor>> _professorRepositorioMock;
		private readonly Mock<IRepositorio<Disciplina>> _disciplinaRepositorioMock;

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

			this._professorServico = new ProfessorServico(this._professorRepositorioMock.Object, this._disciplinaRepositorioMock.Object);

			// Criação de Disciplinas
			this._disciplinaDto1 = new DisciplinaDto() { Nome = "Linguagens Formais e Automatos", Periodo = 1 };
			this._disciplinaDto2 = new DisciplinaDto() { Nome = "Teoria em Grafos", Periodo = 2 };
			this._disciplinaDto3 = new DisciplinaDto() { Nome = "Projeto Integrador", Periodo = 3 };

			this._disciplina1 = Disciplina.Criar(_disciplinaDto1);
			this._disciplina2 =	Disciplina.Criar(_disciplinaDto2);
			this._disciplina3 =	Disciplina.Criar(_disciplinaDto3);
		}

		[Fact(DisplayName = "Erro Ao Alterar Professor - Id nulo ou inválido")]
		public void ErroAoAlterarProfessor_IdProfessorNuloInvalido()
		{
			var professorDto = new AlterarProfessorDto() { Matricula = 2017100150, Nome = "Paulo Roberto", Disciplinas = new List<Guid>() { _disciplina1.ID, _disciplina2.ID, _disciplina3.ID } };

			var exception = Assert.Throws<ArgumentNullException>(() => this._professorServico.AlterarProfessor(Guid.Empty, professorDto));
			Assert.Equal("Id nulo do Professor (não foi informado).", exception.Message);

			this._professorRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Professor>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Alterar Professor - Professor não existe")]
		public void ErroAoAlterarProfessor_ProfessorNaoExiste()
		{
			var professorId = Guid.NewGuid();
			var professorDto = new AlterarProfessorDto() { Matricula = 2017100150, Nome = "Paulo Roberto", Disciplinas = new List<Guid>() { _disciplina1.ID, _disciplina2.ID, _disciplina3.ID } };

			var exception = Assert.Throws<RecursoInexistenteException>(() => this._professorServico.AlterarProfessor(professorId, professorDto));
			Assert.Equal($"Professor com ID '{professorId}' não existe.", exception.Message);

			this._professorRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Professor>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Remover Professor - Id Nulo")]
		public void ErroAoExcluirProfessor_IdNulo()
		{
			var exception = Assert.Throws<ArgumentNullException>(() => this._professorServico.Remover(Guid.Empty));
			Assert.Equal("Id nulo do Professor (não foi informado).", exception.Message);

			this._professorRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Professor>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Remover Professor - Professor não existe")]
		public void ErroAoExcluirProfessor_ProfessorNaoExiste()
		{
			Guid guid = Guid.NewGuid();
			var exception = Assert.Throws<RecursoInexistenteException>(() => this._professorServico.Remover(guid));
			Assert.Equal($"Professor com ID '{guid}' não existe.", exception.Message);

			this._professorRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Professor>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Obter Professor - Por ID - Professor não existe")]
		public void ErroAoObterProfessor_PorID_ProfessorNaoExiste()
		{
			Guid guid = Guid.NewGuid();
			var exception = Assert.Throws<RecursoInexistenteException>(() => this._professorServico.ObterPorId(guid));
			Assert.Equal($"Professor com ID '{guid}' não existe.", exception.Message);

			this._professorRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Professor>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Obter Professor - Id Nulo")]
		public void ErroAoObterProfessor_IdNulo()
		{
			var exception = Assert.Throws<ArgumentNullException>(() => this._professorServico.ObterPorId(Guid.Empty));
			Assert.Equal("Id nulo do Professor (não foi informado).", exception.Message);

			this._professorRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Professor>(), It.IsAny<bool>()), Times.Never);
		}
	}
}