using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Aplicacao.Professores.Remover;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dados.Comum;
using SmartSchool.Dados.Contextos;
using SmartSchool.Dados.Modulos.Disciplinas;
using SmartSchool.Dados.Modulos.Professores;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Disciplinas.Servicos;
using SmartSchool.Dominio.Professores;
using SmartSchool.Dominio.Professores.Servicos;
using Xunit;

namespace SmartSchool.Testes.API.Controllers.Professores
{
	public class RemoverProfessorTestes : TesteApi
	{
		private readonly IUnidadeDeTrabalho _contextos;
		private readonly IMediator _mediator;

		private readonly ProfessorBuilder _professorBuilder;

		public RemoverProfessorTestes()
		{
			this._contextos = ContextoFactory.Criar();
			this._professorBuilder = new ProfessorBuilder();

			var professorRepositorio = new ProfessorRepositorio(this._contextos);

			var professorServicoDominio = new ProfessorServicoDominio(professorRepositorio);

			var serviceProvider = GetServiceProviderComMediatR((typeof(IRepositorio<Professor>), professorRepositorio),
				(typeof(IProfessorServicoDominio), professorServicoDominio));

			this._mediator = serviceProvider.GetRequiredService<IMediator>();
		}

		[Fact(DisplayName = "Remove Professor")]
		public async void DeveRemoverProfessor()
		{
			var professor = this._professorBuilder.ObterProfessor();

			var professorDto = new RemoverProfessorCommand() { ID = professor.ID };

			var retorno = await this._mediator.Send(professorDto);

			retorno.Status.Should().Be(Result.Success().Status);
		}
	}
}