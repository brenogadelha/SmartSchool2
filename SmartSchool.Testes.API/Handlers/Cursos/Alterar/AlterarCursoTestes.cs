using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Aplicacao.Cursos.Alterar;
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
	public class AlterarCursoTestes : TesteApi
	{
		private readonly IUnidadeDeTrabalho _contextos;
		private readonly IMediator _mediator;

		private readonly DisciplinaDto _disciplinaDto3;

		private readonly Disciplina _disciplina3;

		private readonly CursoBuilder _cursoBuilder;

		public AlterarCursoTestes()
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

			// Criação de Disciplina
			this._disciplinaDto3 = new DisciplinaDto() { Nome = "Projeto Integrador", Periodo = 3 };

			this._disciplina3 = Disciplina.Criar(_disciplinaDto3);

			this._contextos.SmartContexto.Disciplinas.Add(_disciplina3);
			this._contextos.SmartContexto.SaveChanges();
		}

		[Fact(DisplayName = "Altera Curso com sucesso")]
		public async void DeveAlterarCurso()
		{
			var curso = this._cursoBuilder.ObterCurso();

			// instancia alteração	
			var cursoDtoAlteracao = new AlterarCursoCommand() { Nome = "Ciência da Computação", DisciplinasId = new List<Guid> { this._disciplina3.ID }, ID = curso.ID };

			var retornoAlteracao = await this._mediator.Send(cursoDtoAlteracao);

			retornoAlteracao.Status.Should().Be(Result.Success().Status);
		}
	}
}