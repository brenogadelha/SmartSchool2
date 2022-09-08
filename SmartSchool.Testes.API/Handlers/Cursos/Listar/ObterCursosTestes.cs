using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Aplicacao.Alunos.Listar;
using SmartSchool.Aplicacao.Cursos.Listar;
using SmartSchool.Aplicacao.Cursos.ObterPorId;
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
using SmartSchool.Dto.Alunos.Obter;
using SmartSchool.Dto.Curso;
using SmartSchool.Dto.Disciplinas;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SmartSchool.Testes.API.Controllers.Cursos
{
	public class ObterCursosTestes : TesteApi
	{
		private readonly IUnidadeDeTrabalho _contextos;
		private readonly IMediator _mediator;

		private readonly DisciplinaDto _disciplinaDto1;

		private readonly Disciplina _disciplina1;

		private readonly CursoBuilder _cursoBuilder;

		public ObterCursosTestes()
		{
			this._contextos = ContextoFactory.Criar();
			this._cursoBuilder = new CursoBuilder();

			var cursoRepositorio = new CursoRepositorio(this._contextos);
			var disciplinaRepositorio = new DisciplinaRepositorio(this._contextos);

			var cursoDominio = new CursoServicoDominio(cursoRepositorio);
			var disciplinaDominio = new DisciplinaServicoDominio(disciplinaRepositorio);

			var serviceProvider = GetServiceProviderComMediatR((typeof(IRepositorio<Curso>), cursoRepositorio),
				(typeof(ICursoServicoDominio), cursoDominio), (typeof(IDisciplinaServicoDominio), disciplinaDominio));

			this._mediator = serviceProvider.GetRequiredService<IMediator>();

			// Criação de Disciplinas
			this._disciplinaDto1 = new DisciplinaDto() { Nome = "Linguagens Formais e Automatos", Periodo = 1 };

			this._disciplina1 = Disciplina.Criar(_disciplinaDto1);

			var cursoDto1 = new CursoDto() { Nome = "Ciência da Computação", DisciplinasId = new List<Guid> { this._disciplina1.ID } };
			var cursoDto2 = new CursoDto() { Nome = "Análise de Sistemas", DisciplinasId = new List<Guid> { this._disciplina1.ID } };
			var cursoDto3 = new CursoDto() { Nome = "Redes", DisciplinasId = new List<Guid> { this._disciplina1.ID } };

			var curso = Curso.Criar(cursoDto1);
			var curso2 = Curso.Criar(cursoDto2);
			var curso3 = Curso.Criar(cursoDto3);

			this._contextos.SmartContexto.Disciplinas.Add(_disciplina1);
			this._contextos.SmartContexto.Cursos.Add(curso);
			this._contextos.SmartContexto.Cursos.Add(curso2);
			this._contextos.SmartContexto.Cursos.Add(curso3);
			this._contextos.SmartContexto.SaveChangesAsync();
		}

		[Fact(DisplayName = "Obtém Cursos com sucesso")]
		public async void DeveListarCursos()
		{			
			var requestCursos = await this._mediator.Send(new ListarCursosCommand());
			var resultCursosObtidos = requestCursos.Should().BeOfType<Result<IEnumerable<ObterCursoDto>>>().Subject;

			resultCursosObtidos.Value.Should().NotBeNull();
			resultCursosObtidos.Value.Count().Should().Be(4);
			resultCursosObtidos.Value.Where(x => x.Nome == "Engenharia da Computação").Count().Should().Be(1);
			resultCursosObtidos.Value.Where(x => x.Nome == "Ciência da Computação").Count().Should().Be(1);
			resultCursosObtidos.Value.Where(x => x.Nome == "Análise de Sistemas").Count().Should().Be(1);
			resultCursosObtidos.Value.Where(x => x.Nome == "Redes").Count().Should().Be(1);
		}
	}
}