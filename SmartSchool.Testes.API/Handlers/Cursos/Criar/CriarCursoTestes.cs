using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using SmartSchool.Aplicacao.Cursos.Adicionar;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dados.Comum;
using SmartSchool.Dados.Contextos;
using SmartSchool.Dados.Modulos.Cursos;
using SmartSchool.Dados.Modulos.Disciplinas;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Cursos;
using SmartSchool.Dominio.Cursos.Servicos;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Disciplinas.Servicos;
using SmartSchool.Dto.Disciplinas;
using System;
using System.Collections.Generic;
using Xunit;

namespace SmartSchool.Testes.API.Controllers.Cursos
{
	public class CriarCursoTestes : TesteApi
	{
		private readonly IUnidadeDeTrabalho _contextos;
		private readonly IMediator _mediator;

		private readonly DisciplinaDto _disciplinaDto1;
		private readonly DisciplinaDto _disciplinaDto2;
		private readonly DisciplinaDto _disciplinaDto3;

		private readonly Disciplina _disciplina1;
		private readonly Disciplina _disciplina2;
		private readonly Disciplina _disciplina3;

		public CriarCursoTestes()
		{
			this._contextos = ContextoFactory.Criar();

			var cursoRepositorio = new CursoRepositorio(this._contextos);
			var disciplinaRepositorio = new DisciplinaRepositorio(this._contextos);

			var cursoDominio = new CursoServicoDominio(cursoRepositorio);
			var disciplinaDominio = new DisciplinaServicoDominio(disciplinaRepositorio);

			var serviceProvider = GetServiceProviderComMediatR((typeof(IRepositorio<Curso>), cursoRepositorio), 
				(typeof(ICursoServicoDominio), cursoDominio), (typeof(IDisciplinaServicoDominio), disciplinaDominio));

			this._mediator = serviceProvider.GetRequiredService<IMediator>();

			// Criação de Disciplinas
			this._disciplinaDto1 = new DisciplinaDto() { Nome = "Linguagens Formais e Automatos", Periodo = 1 };
			this._disciplinaDto2 = new DisciplinaDto() { Nome = "Teoria em Grafos", Periodo = 2 };
			this._disciplinaDto3 = new DisciplinaDto() { Nome = "Projeto Integrador", Periodo = 3 };

			this._disciplina1 = Disciplina.Criar(_disciplinaDto1);
			this._disciplina2 = Disciplina.Criar(_disciplinaDto2);
			this._disciplina3 = Disciplina.Criar(_disciplinaDto3);

			this._contextos.SmartContexto.Disciplinas.Add(_disciplina1);
			this._contextos.SmartContexto.Disciplinas.Add(_disciplina2);
			this._contextos.SmartContexto.Disciplinas.Add(_disciplina3);
			this._contextos.SmartContexto.SaveChanges();
		}

		[Fact(DisplayName = "Inclui Curso com sucesso")]
		public async void DeveCriarCurso()
		{
			var disciplinas = new List<Guid>() { _disciplina1.ID, _disciplina2.ID, _disciplina3.ID };

			var cursoDto = new AdicionarCursoCommand() { Nome = "Engenharia da Computação", DisciplinasId = disciplinas };

			var retorno = await this._mediator.Send(cursoDto);

			var result = retorno.Should().BeOfType<Result<Guid>>().Subject;

			result.Value.Should().NotBeEmpty();
		}
	}
}