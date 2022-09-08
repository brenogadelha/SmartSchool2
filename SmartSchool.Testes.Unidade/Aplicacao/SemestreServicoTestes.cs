using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SmartSchool.Aplicacao.Semestres.Alterar;
using SmartSchool.Aplicacao.Semestres.ObterPorId;
using SmartSchool.Aplicacao.Semestres.Remover;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Dominio.Semestres;
using SmartSchool.Dominio.Semestres.Servicos;
using System;
using Xunit;

namespace SmartSchool.Testes.Unidade.Aplicacao
{
	public class SemestreServicoTestes : TesteUnidade
	{
		private readonly IMediator _mediator;

		private readonly Mock<IRepositorio<Semestre>> _semestreRepositorioMock;

		public SemestreServicoTestes()
		{
			this._semestreRepositorioMock = new Mock<IRepositorio<Semestre>>();

			var semestreServicoDominio = new SemestreServicoDominio(this._semestreRepositorioMock.Object);

			var serviceProvider = GetServiceProviderComMediatR((typeof(IRepositorio<Semestre>), this._semestreRepositorioMock.Object), (typeof(ISemestreServicoDominio), semestreServicoDominio));

			this._mediator = serviceProvider.GetRequiredService<IMediator>();
		}

		[Fact(DisplayName = "Erro Ao Alterar Semestre - Id nulo ou inválido")]
		public void ErroAoAlterarSemestre_IdNuloInvalido()
		{
			var semestreDto = new AlterarSemestreCommand() { DataInicio = DateTime.Now.AddDays(10), DataFim = DateTime.Now.AddMonths(5), ID = Guid.Empty };

			var exception = Assert.ThrowsAsync<ArgumentNullException>(() => this._mediator.Send(semestreDto));
			Assert.Equal("Id nulo do Semestre (não foi informado).", exception.Result.Message);

			this._semestreRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Semestre>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Alterar Semestre - Semestre não existe")]
		public void ErroAoAlterarSemestre_NaoExiste()
		{
			var semestreId = Guid.NewGuid();

			var semestreDto = new AlterarSemestreCommand() { DataInicio = DateTime.Now.AddDays(10), DataFim = DateTime.Now.AddMonths(5), ID = semestreId };

			var exception = Assert.ThrowsAsync<RecursoInexistenteException>(() => this._mediator.Send(semestreDto));
			Assert.Equal($"Semestre com ID '{semestreId}' não existe.", exception.Result.Message);

			this._semestreRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Semestre>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Remover Semestre - Id Nulo")]
		public void ErroAoExcluirSemestre_IdNulo()
		{
			var exception = Assert.ThrowsAsync<ArgumentNullException>(() => this._mediator.Send(new RemoverSemestreCommand { ID = Guid.Empty }));
			Assert.Equal("Id nulo do Semestre (não foi informado).", exception.Result.Message);

			this._semestreRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Semestre>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Remover Curso - Curso não existe")]
		public void ErroAoExcluirSemestre_NaoExiste()
		{
			Guid id = Guid.NewGuid();
			var exception = Assert.ThrowsAsync<RecursoInexistenteException>(() => this._mediator.Send(new RemoverSemestreCommand { ID = id }));
			Assert.Equal($"Semestre com ID '{id}' não existe.", exception.Result.Message);

			this._semestreRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Semestre>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Obter Semestre - Por ID - Semestre não existe")]
		public void ErroAoObterSemestre_PorID_NaoExiste()
		{
			Guid id = Guid.NewGuid();
			var exception = Assert.ThrowsAsync<RecursoInexistenteException>(() => this._mediator.Send(new ObterSemestreCommand { Id = id }));
			Assert.Equal($"Semestre com ID '{id}' não existe.", exception.Result.Message);

			this._semestreRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Semestre>(), It.IsAny<bool>()), Times.Never);
		}

		[Fact(DisplayName = "Erro Ao Obter Semestre - Id Nulo")]
		public void ErroAoObterSemestre_IdNulo()
		{
			var exception = Assert.ThrowsAsync<ArgumentNullException>(() => this._mediator.Send(new ObterSemestreCommand { Id = Guid.Empty }));
			Assert.Equal("Id nulo do Semestre (não foi informado).", exception.Result.Message);

			this._semestreRepositorioMock.Verify(x => x.Atualizar(It.IsAny<Semestre>(), It.IsAny<bool>()), Times.Never);
		}
	}
}