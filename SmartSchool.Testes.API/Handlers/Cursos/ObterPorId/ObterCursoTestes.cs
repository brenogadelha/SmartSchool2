using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Aplicacao.Cursos.ObterPorId;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dados.Comum;
using SmartSchool.Dados.Contextos;
using SmartSchool.Dados.Modulos.Cursos;
using SmartSchool.Dados.Modulos.Disciplinas;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Cursos;
using SmartSchool.Dominio.Cursos.Servicos;
using SmartSchool.Dominio.Disciplinas.Servicos;
using SmartSchool.Dto.Curso;
using System;
using System.Linq;
using Xunit;

namespace SmartSchool.Testes.API.Controllers.Cursos
{
	public class ObterCursoTestes : TesteApi
	{
		private readonly IUnidadeDeTrabalho _contextos;
		private readonly IMediator _mediator;

		private readonly CursoBuilder _cursoBuilder;

		public ObterCursoTestes()
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
		}

		[Fact(DisplayName = "Obtém Curso com sucesso")]
		public async void DeveObterCurso()
		{
			var curso = this._cursoBuilder.ObterCurso();

			var retornoAlteracao = await this._mediator.Send(new ObterCursoQuery { Id = curso.ID });
			var resultCursoObtidoPorId = retornoAlteracao.Should().BeOfType<Result<ObterCursoDto>>().Subject;

			resultCursoObtidoPorId.Value.Should().NotBeNull();
			resultCursoObtidoPorId.Value.ID.Should().NotBe(Guid.Empty);
			resultCursoObtidoPorId.Value.Nome.Should().Be(curso.Nome);
			resultCursoObtidoPorId.Value.Disciplinas.Where(x => x == "Linguagens Formais e Automatos").Count().Should().Be(1);
			resultCursoObtidoPorId.Value.Disciplinas.Where(x => x == "Teoria em Grafos").Count().Should().Be(1);
			resultCursoObtidoPorId.Value.Disciplinas.Count().Should().Be(2);
		}
	}
}