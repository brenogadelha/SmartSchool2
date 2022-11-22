using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Aplicacao.Disciplinas.ObterProfessores;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dados.Comum;
using SmartSchool.Dados.Contextos;
using SmartSchool.Dados.Modulos.Disciplinas;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Disciplinas.Servicos;
using SmartSchool.Dto.Dtos.Professores;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SmartSchool.Testes.API.Controllers.Disciplinas
{
	public class ObterProfessoresDisciplinaTestes : TesteApi
	{
		private readonly IUnidadeDeTrabalho _contextos;
		private readonly IMediator _mediator;

		private readonly DisciplinaBuilder _disciplinaBuilder;

		public ObterProfessoresDisciplinaTestes()
		{
			this._contextos = ContextoFactory.Criar();
			this._disciplinaBuilder = new DisciplinaBuilder();

			var disciplinaRepositorio = new DisciplinaRepositorio(this._contextos);

			var disciplinaServico = new DisciplinaServicoDominio(disciplinaRepositorio);

			var serviceProvider = GetServiceProviderComMediatR((typeof(IRepositorio<Disciplina>), disciplinaRepositorio), (typeof(IDisciplinaServicoDominio), disciplinaServico));

			this._mediator = serviceProvider.GetRequiredService<IMediator>();
		}

		[Fact(DisplayName = "Lista Professores de uma Disciplina")]
		public async void DeveListarProfessoresDisciplina()
		{
			var disciplina = this._disciplinaBuilder.ObterDisciplina();

			var retorno = await this._mediator.Send(new ObterProfessoresDisciplinaQuery { Id = disciplina.ID });

			var resultDisciplinas = retorno.Should().BeOfType<Result<IEnumerable<ObterProfessorLightDto>>>().Subject;

			resultDisciplinas.Value.Should().NotBeNull();
			resultDisciplinas.Value.Count().Should().Be(2);
			resultDisciplinas.Value.Select(x => x.ID).Should().NotBeNull();
			resultDisciplinas.Value.Where(x => x.Nome == "José Paulo").Count().Should().Be(1);
			resultDisciplinas.Value.Where(x => x.Nome == "Paulo Roberto").Count().Should().Be(1);
		}
	}
}