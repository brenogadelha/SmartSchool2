using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Aplicacao.Professores.Adicionar;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dados.Comum;
using SmartSchool.Dados.Contextos;
using SmartSchool.Dados.Modulos.Disciplinas;
using SmartSchool.Dados.Modulos.Professores;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Disciplinas.Servicos;
using SmartSchool.Dominio.Professores;
using SmartSchool.Dominio.Professores.Servicos;
using SmartSchool.Dto.Disciplinas;
using System;
using System.Collections.Generic;
using Xunit;

namespace SmartSchool.Testes.API.Controllers.Professores
{
	public class AdicionarProfessorTestes : TesteApi
	{
		private readonly IUnidadeDeTrabalho _contextos;
		private readonly IMediator _mediator;

		private readonly Disciplina _disciplina;
		private readonly Disciplina _disciplina2;
		private readonly Disciplina _disciplina3;

		public AdicionarProfessorTestes()
		{
			this._contextos = ContextoFactory.Criar();

			var professorRepositorio = new ProfessorRepositorio(this._contextos);
			var disciplinaRepositorio = new DisciplinaRepositorio(this._contextos);

			var disciplinaServicoDominio = new DisciplinaServicoDominio(disciplinaRepositorio);
			var professorServicoDominio = new ProfessorServicoDominio(professorRepositorio);

			var serviceProvider = GetServiceProviderComMediatR((typeof(IRepositorio<Professor>), professorRepositorio), 
				(typeof(IProfessorServicoDominio), professorServicoDominio), (typeof(IDisciplinaServicoDominio), disciplinaServicoDominio));

			this._mediator = serviceProvider.GetRequiredService<IMediator>();

			// Criação de Disciplinas
			var disciplinaDto1 = new DisciplinaDto() { Nome = "Linguagens Formais e Automatoss", Periodo = 1 };
			var disciplinaDto2 = new DisciplinaDto() { Nome = "Teoria em Grafoss", Periodo = 2 };
			var disciplinaDto3 = new DisciplinaDto() { Nome = "Projeto Integradorr", Periodo = 3 };

			this._disciplina = Disciplina.Criar(disciplinaDto1);
			this._disciplina2 = Disciplina.Criar(disciplinaDto2);
			this._disciplina3 = Disciplina.Criar(disciplinaDto3);

			this._contextos.SmartContexto.Disciplinas.Add(this._disciplina); ;
			this._contextos.SmartContexto.Disciplinas.Add(this._disciplina2);
			this._contextos.SmartContexto.Disciplinas.Add(this._disciplina3);
			this._contextos.SmartContexto.SaveChangesAsync();
		}

		[Fact(DisplayName = "Adiciona Professor")]
		public async void DeveCriarProfessor()
		{
			var professorDto = new AdicionarProfessorCommand() { Matricula = 2017100150, Nome = "Paulo Roberrto", Email = "pauloroberto@unicarioca.com.br", Disciplinas = new List<Guid>() { this._disciplina.ID, this._disciplina2.ID, this._disciplina3.ID } };

			var retorno = await this._mediator.Send(professorDto);

			var result = retorno.Should().BeOfType<Result<Guid>>().Subject;

			result.Value.Should().NotBeEmpty();
		}
	}
}