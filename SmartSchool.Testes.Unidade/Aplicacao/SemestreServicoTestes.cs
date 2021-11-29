using Moq;
using SmartSchool.Aplicacao.Semestres.Interface;
using SmartSchool.Aplicacao.Semestres.Servico;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Dominio.Semestres;
using SmartSchool.Dto.Semestres;
using System;
using Xunit;

namespace SmartSchool.Testes.Unidade.Aplicacao
{
	public class SemestreServicoTestes : TesteUnidade
	{
		private readonly ISemestreServico _semestreServico;

		private readonly Mock<IRepositorio<Semestre>> _semestreRepositorioMock;

		public SemestreServicoTestes()
		{
			this._semestreRepositorioMock = new Mock<IRepositorio<Semestre>>();

			this._semestreServico = new SemestreServico(this._semestreRepositorioMock.Object);
		}

		[Fact(DisplayName = "Erro Ao Alterar Semestre - Id nulo ou inválido")]
		public void ErroAoAlterarSemestre_IdNuloInvalido()
		{
			var semestreDto = new AlterarObterSemestreDto() { DataInicio = DateTime.Now.AddDays(10), DataFim = DateTime.Now.AddMonths(5) };

			var exception = Assert.Throws<ArgumentNullException>(() => this._semestreServico.AlterarSemestre(Guid.Empty, semestreDto));
			Assert.Equal("Id nulo do Semestre (não foi informado).", exception.Message);

			this._semestreRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Semestre>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Alterar Semestre - Curso não existe")]
		public void ErroAoAlterarSemestre_NaoExiste()
		{
			var semestreId = Guid.NewGuid();

			var semestreDto = new AlterarObterSemestreDto() { DataInicio = DateTime.Now.AddDays(10), DataFim = DateTime.Now.AddMonths(5) };

			var exception = Assert.Throws<RecursoInexistenteException>(() => this._semestreServico.AlterarSemestre(semestreId, semestreDto));
			Assert.Equal($"Semestre com ID '{semestreId}' não existe.", exception.Message);

			this._semestreRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Semestre>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Remover Semestre - Id Nulo")]
		public void ErroAoExcluirSemestre_IdNulo()
		{
			var exception = Assert.Throws<ArgumentNullException>(() => this._semestreServico.Remover(Guid.Empty));
			Assert.Equal("Id nulo do Semestre (não foi informado).", exception.Message);

			this._semestreRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Semestre>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Remover Curso - Curso não existe")]
		public void ErroAoExcluirSemestre_NaoExiste()
		{
			Guid guid = Guid.NewGuid();
			var exception = Assert.Throws<RecursoInexistenteException>(() => this._semestreServico.Remover(guid));
			Assert.Equal($"Semestre com ID '{guid}' não existe.", exception.Message);

			this._semestreRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Semestre>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Obter Semestre - Por ID - Semestre não existe")]
		public void ErroAoObterSemestre_PorID_NaoExiste()
		{
			Guid guid = Guid.NewGuid();
			var exception = Assert.Throws<RecursoInexistenteException>(() => this._semestreServico.ObterPorId(guid));
			Assert.Equal($"Semestre com ID '{guid}' não existe.", exception.Message);

			this._semestreRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Semestre>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Obter Semestre - Id Nulo")]
		public void ErroAoObterSemestre_IdNulo()
		{
			var exception = Assert.Throws<ArgumentNullException>(() => this._semestreServico.ObterPorId(Guid.Empty));
			Assert.Equal("Id nulo do Semestre (não foi informado).", exception.Message);

			this._semestreRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Semestre>(), It.IsAny<bool>()), Times.Never);
		}
	}
}