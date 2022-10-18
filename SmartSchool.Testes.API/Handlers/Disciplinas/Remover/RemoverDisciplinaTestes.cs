using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Aplicacao.Disciplinas.Remover;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dados.Comum;
using SmartSchool.Dados.Contextos;
using SmartSchool.Dados.Modulos.Disciplinas;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Disciplinas.Servicos;
using Xunit;

namespace SmartSchool.Testes.API.Controllers.Disciplinas
{
	public class RemoverDisciplinaTestes : TesteApi
	{
		private readonly IUnidadeDeTrabalho _contextos;
		private readonly IMediator _mediator;

		private readonly DisciplinaBuilder _disciplinaBuilder;

		public RemoverDisciplinaTestes()
		{
			this._contextos = ContextoFactory.Criar();
			this._disciplinaBuilder = new DisciplinaBuilder();

			var disciplinaRepositorio = new DisciplinaRepositorio(this._contextos);

			var disciplinaServico = new DisciplinaServicoDominio(disciplinaRepositorio);

			var serviceProvider = GetServiceProviderComMediatR((typeof(IRepositorio<Disciplina>), disciplinaRepositorio), (typeof(IDisciplinaServicoDominio), disciplinaServico));

			this._mediator = serviceProvider.GetRequiredService<IMediator>();
		}

		[Fact(DisplayName = "Remove Disciplina")]
		public async void DeveRemoverDisciplina()
		{
			var disciplina = this._disciplinaBuilder.ObterDisciplina();

			var requestRemoveCurso = await this._mediator.Send(new RemoverDisciplinaCommand { ID = disciplina.ID });

			requestRemoveCurso.Status.Should().Be(Result.Success().Status);
		}
	}
}