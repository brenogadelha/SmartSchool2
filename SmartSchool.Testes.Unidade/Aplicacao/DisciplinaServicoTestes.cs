using Moq;
using SmartSchool.Aplicacao.Disciplinas.Interface;
using SmartSchool.Aplicacao.Disciplinas.Servico;
using SmartSchool.Aplicacao.Professores.Interface;
using SmartSchool.Aplicacao.Professores.Servico;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Professores;
using SmartSchool.Dto.Disciplinas;
using SmartSchool.Dto.Disciplinas.Alterar;
using SmartSchool.Dto.Dtos.Professores;
using System;
using System.Collections.Generic;
using Xunit;

namespace SmartSchool.Testes.Unidade.Aplicacao
{
	public class DisciplinaServicoTestes : TesteUnidade
	{
		private readonly IDisciplinaServico _disciplinaServico;

		private readonly Mock<IRepositorio<Disciplina>> _disciplinaRepositorioMock;

		public DisciplinaServicoTestes()
		{
			this._disciplinaRepositorioMock = new Mock<IRepositorio<Disciplina>>();

			this._disciplinaServico = new DisciplinaServico(this._disciplinaRepositorioMock.Object);
		}

		[Fact(DisplayName = "Erro Ao Alterar Disciplina - Id nulo ou inválido")]
		public void ErroAoAlterarDisciplina_IdNuloInvalido()
		{
			var disciplinaDto = new AlterarDisciplinaDto() { Nome = "Linguagens Formais e Automatos", Periodo = 1 };

			var exception = Assert.Throws<ArgumentNullException>(() => this._disciplinaServico.AlterarDisciplina(Guid.Empty, disciplinaDto));
			Assert.Equal("Id nulo da Disciplina (não foi informado).", exception.Message);

			this._disciplinaRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Disciplina>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Alterar Disciplina - Disciplina não existe")]
		public void ErroAoAlterarDisciplina_NaoExiste()
		{
			var disciplinaId = Guid.NewGuid();
			var disciplinaDto = new AlterarDisciplinaDto() { Nome = "Linguagens Formais e Automatos", Periodo = 1 };

			var exception = Assert.Throws<RecursoInexistenteException>(() => this._disciplinaServico.AlterarDisciplina(disciplinaId, disciplinaDto));
			Assert.Equal($"Disciplina com ID '{disciplinaId}' não existe.", exception.Message);

			this._disciplinaRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Disciplina>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Remover Disciplina - Id Nulo")]
		public void ErroAoExcluirDisciplina_IdNulo()
		{
			var exception = Assert.Throws<ArgumentNullException>(() => this._disciplinaServico.Remover(Guid.Empty));
			Assert.Equal("Id nulo da Disciplina (não foi informado).", exception.Message);

			this._disciplinaRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Disciplina>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Remover Disciplina - Disciplina não existe")]
		public void ErroAoExcluirDisciplina_NaoExiste()
		{
			Guid guid = Guid.NewGuid();
			var exception = Assert.Throws<RecursoInexistenteException>(() => this._disciplinaServico.Remover(guid));
			Assert.Equal($"Disciplina com ID '{guid}' não existe.", exception.Message);

			this._disciplinaRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Disciplina>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Obter Disciplina - Por ID - Disciplina não existe")]
		public void ErroAoObterDisciplina_PorID_NaoExiste()
		{
			Guid guid = Guid.NewGuid();
			var exception = Assert.Throws<RecursoInexistenteException>(() => this._disciplinaServico.ObterPorId(guid));
			Assert.Equal($"Disciplina com ID '{guid}' não existe.", exception.Message);

			this._disciplinaRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Disciplina>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Obter Disciplina - Id Nulo")]
		public void ErroAoObterDisciplina_IdNulo()
		{
			var exception = Assert.Throws<ArgumentNullException>(() => this._disciplinaServico.ObterPorId(Guid.Empty));
			Assert.Equal("Id nulo da Disciplina (não foi informado).", exception.Message);

			this._disciplinaRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Disciplina>(), It.IsAny<bool>()), Times.Never);
		}
	}
}