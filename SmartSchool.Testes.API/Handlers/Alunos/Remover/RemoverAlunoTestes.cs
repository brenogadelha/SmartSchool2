using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Aplicacao.Alunos.ObterPorId;
using SmartSchool.Aplicacao.Alunos.ObterPorMatricula;
using SmartSchool.Aplicacao.Alunos.RemoverAluno;
using SmartSchool.Comum.Dominio.Enums;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dados.Comum;
using SmartSchool.Dados.Contextos;
using SmartSchool.Dados.Modulos.Alunos;
using SmartSchool.Dados.Modulos.Cursos;
using SmartSchool.Dados.Modulos.Disciplinas;
using SmartSchool.Dados.Modulos.Semestres;
using SmartSchool.Dominio.Alunos;
using SmartSchool.Dominio.Alunos.Servicos;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Cursos;
using SmartSchool.Dominio.Cursos.Servicos;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Disciplinas.Servicos;
using SmartSchool.Dominio.Semestres;
using SmartSchool.Dominio.Semestres.Servicos;
using SmartSchool.Dto.Alunos;
using SmartSchool.Dto.Alunos.Obter;
using SmartSchool.Dto.Curso;
using SmartSchool.Dto.Disciplinas;
using SmartSchool.Dto.Semestres;
using SmartSchool.Testes.Compartilhado.Builders;
using System;
using System.Collections.Generic;
using Xunit;

namespace SmartSchool.Testes.API.Controllers.Alunos
{
	public class RemoverAlunoTestes : TesteApi
	{
		private readonly IUnidadeDeTrabalho _contextos;
		private readonly IMediator _mediator;
		private readonly AlunoBuilder _alunoBuilder;

		public RemoverAlunoTestes()
		{
			this._alunoBuilder = new AlunoBuilder();
			this._contextos = ContextoFactory.Criar();

			var alunoRepositorioTask = new AlunoRepositorio(this._contextos);
			var cursoRepositorioTask = new CursoRepositorio(this._contextos);
			var disciplinaRepositorioTask = new DisciplinaRepositorio(this._contextos);
			var semestreRepositorioTask = new SemestreRepositorio(this._contextos);

			var cursoDominio = new CursoServicoDominio(cursoRepositorioTask);
			var alunoDominio = new AlunoServicoDominio(alunoRepositorioTask);
			var disciplinaDominio = new DisciplinaServicoDominio(disciplinaRepositorioTask);
			var semestreDominio = new SemestreServicoDominio(semestreRepositorioTask);

			var serviceProvider = GetServiceProviderComMediatR((typeof(IRepositorio<Aluno>), alunoRepositorioTask), (typeof(IDisciplinaServicoDominio), disciplinaDominio),
			(typeof(ISemestreServicoDominio), semestreDominio), (typeof(ICursoServicoDominio), cursoDominio), (typeof(IAlunoServicoDominio), alunoDominio));

			this._mediator = serviceProvider.GetRequiredService<IMediator>();						
		}

		[Fact(DisplayName = "Remove Aluno")]
		public async void DeveRemoverAluno()
		{
			var requestRemoveAluno = await this._mediator.Send(new RemoverAlunoCommand { ID = this._alunoBuilder.ObterAluno().ID });

			requestRemoveAluno.Status.Should().Be(Result.Success().Status);
		}
	}
}
