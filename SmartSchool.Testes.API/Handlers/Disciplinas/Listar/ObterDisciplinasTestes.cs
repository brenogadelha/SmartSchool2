using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Aplicacao.Disciplinas.Listar;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dados.Comum;
using SmartSchool.Dados.Contextos;
using SmartSchool.Dados.Modulos.Disciplinas;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Disciplinas.Servicos;
using SmartSchool.Dto.Disciplinas;
using SmartSchool.Dto.Disciplinas.Obter;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SmartSchool.Testes.API.Controllers.Disciplinas
{
	public class ObterDisciplinasTestes : TesteApi
	{
		private readonly IUnidadeDeTrabalho _contextos;
		private readonly IMediator _mediator;

		private readonly DisciplinaBuilder _disciplinaBuilder;

		private readonly Disciplina _disciplina;
		private readonly Disciplina _disciplina2;
		private readonly Disciplina _disciplina3;

		public ObterDisciplinasTestes()
		{
			this._contextos = ContextoFactory.Criar();
			this._disciplinaBuilder = new DisciplinaBuilder();

			var disciplinaRepositorio = new DisciplinaRepositorio(this._contextos);

			var disciplinaServico = new DisciplinaServicoDominio(disciplinaRepositorio);

			var serviceProvider = GetServiceProviderComMediatR((typeof(IRepositorio<Disciplina>), disciplinaRepositorio), (typeof(IDisciplinaServicoDominio), disciplinaServico));

			this._mediator = serviceProvider.GetRequiredService<IMediator>();

			// Criação de Disciplinas
			var disciplinaDto1 = new DisciplinaDto() { Nome = "Cálculo I", Periodo = 1 };
			var disciplinaDto2 = new DisciplinaDto() { Nome = "Cálculo II", Periodo = 2 };
			var disciplinaDto3 = new DisciplinaDto() { Nome = "Cálculo III", Periodo = 3 };

			this._disciplina = Disciplina.Criar(disciplinaDto1);
			this._disciplina2 = Disciplina.Criar(disciplinaDto2);
			this._disciplina3 = Disciplina.Criar(disciplinaDto3);

			this._contextos.SmartContexto.Disciplinas.Add(this._disciplina);
			this._contextos.SmartContexto.Disciplinas.Add(this._disciplina2);
			this._contextos.SmartContexto.Disciplinas.Add(this._disciplina3);
			this._contextos.SmartContexto.SaveChangesAsync();
		}

		[Fact(DisplayName = "Lista Disciplinas")]
		public async void DeveListarDisciplina()
		{
			var disciplina = this._disciplinaBuilder.ObterDisciplina();

			var retorno = await this._mediator.Send(new ListarDisciplinasQuery());

			var resultDisciplinas = retorno.Should().BeOfType<Result<IEnumerable<ObterDisciplinaDto>>>().Subject;

			resultDisciplinas.Value.Should().NotBeNull();
			resultDisciplinas.Value.Count().Should().Be(4);
			resultDisciplinas.Value.Where(x => x.Nome == "Cálculo I").Count().Should().Be(1);
			resultDisciplinas.Value.Where(x => x.Nome == "Cálculo II").Count().Should().Be(1);
			resultDisciplinas.Value.Where(x => x.Nome == "Cálculo III").Count().Should().Be(1);
			resultDisciplinas.Value.Where(x => x.Nome == disciplina.Nome).Count().Should().Be(1);
		}
	}
}