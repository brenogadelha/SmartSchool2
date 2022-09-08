using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Aplicacao.Cursos.Remover;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dados.Comum;
using SmartSchool.Dados.Contextos;
using SmartSchool.Dados.Modulos.Cursos;
using SmartSchool.Dados.Modulos.Disciplinas;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Cursos;
using SmartSchool.Dominio.Cursos.Servicos;
using SmartSchool.Dominio.Disciplinas.Servicos;
using Xunit;

namespace SmartSchool.Testes.API.Controllers.Cursos
{
	public class RemoverCursoTestes : TesteApi
	{
		private readonly IUnidadeDeTrabalho _contextos;
		private readonly IMediator _mediator;

		private readonly CursoBuilder _cursoBuilder;

		public RemoverCursoTestes()
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

		[Fact(DisplayName = "Remove Curso com sucesso")]
		public async void DeveRemoverCurso()
		{
			var requestRemoveCurso = await this._mediator.Send(new RemoverCursoCommand { ID = this._cursoBuilder.ObterCurso().ID });

			requestRemoveCurso.Status.Should().Be(Result.Success().Status);
		}
	}
}