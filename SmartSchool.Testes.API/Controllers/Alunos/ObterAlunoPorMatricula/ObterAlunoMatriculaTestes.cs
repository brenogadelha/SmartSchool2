using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Aplicacao.Alunos.ObterAlunoMatricula;
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
using SmartSchool.Dominio.Cursos.Servicos;
using SmartSchool.Dominio.Disciplinas.Servicos;
using SmartSchool.Dominio.Semestres.Servicos;
using SmartSchool.Dto.Alunos;
using SmartSchool.Dto.Alunos.Obter;
using SmartSchool.Testes.Compartilhado.Builders;
using System;
using Xunit;

namespace SmartSchool.Testes.API.Controllers.Alunos.ObterAlunoPorMatricula
{
	public class ObterAlunoMatriculaTestes : TesteApi
	{
		private readonly IUnidadeDeTrabalho _contextos;
		private readonly IMediator _mediator;

		private readonly AlunoBuilder _alunoBuilder;

		public ObterAlunoMatriculaTestes()
		{
			this._alunoBuilder = new AlunoBuilder();
			this._contextos = ContextoFactory.Criar();

			var alunoRepositorioTask = new AlunoRepositorioTask(this._contextos);
			var cursoRepositorioTask = new CursoRepositorioTask(this._contextos);
			var disciplinaRepositorioTask = new DisciplinaRepositorioTask(this._contextos);
			var semestreRepositorioTask = new SemestreRepositorioTask(this._contextos);

			var cursoDominio = new CursoServicoDominio(cursoRepositorioTask);
			var alunoDominio = new AlunoServicoDominio(alunoRepositorioTask);
			var disciplinaDominio = new DisciplinaServicoDominio(disciplinaRepositorioTask);
			var semestreDominio = new SemestreServicoDominio(semestreRepositorioTask);

			var serviceProvider = GetServiceProviderComMediatR((typeof(IRepositorioTask<Aluno>), alunoRepositorioTask), (typeof(IDisciplinaServicoDominio), disciplinaDominio),
			(typeof(ISemestreServicoDominio), semestreDominio), (typeof(ICursoServicoDominio), cursoDominio), (typeof(IAlunoServicoDominio), alunoDominio));

			this._mediator = serviceProvider.GetRequiredService<IMediator>();
		}

		[Fact(DisplayName = "Obtém Aluno por Matrícula")]
		public async void DeveObterPorMatricula()
		{
			var aluno = this._alunoBuilder.ObterAluno();

			// Obtem por Matrícula
			var requestAlunoPorMatricula = await this._mediator.Send(new ObterAlunoMatriculaCommand { Matricula = aluno.Matricula });
			var resultAlunoObtidoPorMatricula = requestAlunoPorMatricula.Should().BeOfType<Result<ObterAlunoDto>>().Subject;

			resultAlunoObtidoPorMatricula.Value.Should().NotBeNull();
			resultAlunoObtidoPorMatricula.Value.ID.Should().NotBe(Guid.Empty);
			resultAlunoObtidoPorMatricula.Value.Nome.Should().Be(aluno.Nome);
			resultAlunoObtidoPorMatricula.Value.Ativo.Should().Be(true);
			resultAlunoObtidoPorMatricula.Value.Sobrenome.Should().Be(aluno.Sobrenome);
			resultAlunoObtidoPorMatricula.Value.Celular.Should().Be(aluno.Celular);
			resultAlunoObtidoPorMatricula.Value.Endereco.Should().Be(aluno.Endereco);
			resultAlunoObtidoPorMatricula.Value.Cidade.Should().Be(aluno.Cidade);
			resultAlunoObtidoPorMatricula.Value.Cpf.Should().Be(aluno.Cpf);
			resultAlunoObtidoPorMatricula.Value.DataNascimento.ToString().Should().Contain(aluno.DataNascimento.ToString("dd/MM/yyyy"));
			resultAlunoObtidoPorMatricula.Value.DataInicio.ToString().Should().Contain(aluno.DataInicio.ToString("dd/MM/yyyy"));
			resultAlunoObtidoPorMatricula.Value.DataFim.ToString().Should().Contain(aluno.DataFim.ToString("dd/MM/yyyy"));
			resultAlunoObtidoPorMatricula.Value.Email.Should().Be(aluno.Email);
			resultAlunoObtidoPorMatricula.Value.Telefone.Should().Be(aluno.Telefone);
			resultAlunoObtidoPorMatricula.Value.Curso.Should().Be("Engenharia da Computação");
		}
	}
}
