using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Aplicacao.Professores.ObterPorId;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dados.Comum;
using SmartSchool.Dados.Contextos;
using SmartSchool.Dados.Modulos.Professores;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Professores;
using SmartSchool.Dominio.Professores.Servicos;
using SmartSchool.Dto.Dtos.Professores;
using System;
using Xunit;

namespace SmartSchool.Testes.API.Controllers.Professores
{
	public class ObterProfessorTestes : TesteApi
	{
		private readonly IUnidadeDeTrabalho _contextos;
		private readonly IMediator _mediator;

		private readonly ProfessorBuilder _professorBuilder;

		public ObterProfessorTestes()
		{
			this._contextos = ContextoFactory.Criar();
			this._professorBuilder = new ProfessorBuilder();

			var professorRepositorio = new ProfessorRepositorio(this._contextos);

			var professorServicoDominio = new ProfessorServicoDominio(professorRepositorio);

			var serviceProvider = GetServiceProviderComMediatR((typeof(IRepositorio<Professor>), professorRepositorio),
				(typeof(IProfessorServicoDominio), professorServicoDominio));

			this._mediator = serviceProvider.GetRequiredService<IMediator>();
		}

		[Fact(DisplayName = "Obtém Professor por ID")]
		public async void DeveObterProfessor()
		{
			var professor = this._professorBuilder.ObterProfessor();

			var professorDto = new ObterProfessorQuery() { Id = professor.ID };

			var retorno = await this._mediator.Send(professorDto);

			var resultProfessorObtidoPorId = retorno.Should().BeOfType<Result<ObterProfessorDto>>().Subject;

			resultProfessorObtidoPorId.Value.Should().NotBeNull();
			resultProfessorObtidoPorId.Value.ID.Should().NotBe(Guid.Empty);
			resultProfessorObtidoPorId.Value.Nome.Should().Be(professor.Nome);
			resultProfessorObtidoPorId.Value.Matricula.Should().Be(professor.Matricula);
			resultProfessorObtidoPorId.Value.Email.Should().Be(professor.Email);
		}
	}
}