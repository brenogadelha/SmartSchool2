﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;
using Xunit.DependencyInjection;
using SmartSchool.Comum.Infra.Opcoes;
using SmartSchool.Comum.Configuracao;
using SmartSchool.Comum.Infra;
using SmartSchool.Dados.Contextos;
using SmartSchool.Dados.Comum;
using MediatR;
using SmartSchool.Aplicacao.Alunos.ListarAlunos;
using SmartSchool.Aplicacao.Alunos.ObterAluno;
using Parceiros.Template.Aplicacao.Pessoas.ListarPessoas;
using SmartSchool.Aplicacao.Alunos.AdicionarAluno;
using SmartSchool.Aplicacao.Alunos.AlterarAluno;
using SmartSchool.Aplicacao.Alunos.ObterAlunoMatricula;
using SmartSchool.Aplicacao.Alunos.ObterAlunoNome;
using SmartSchool.Aplicacao.Alunos.ObterHistoricoAluno;
using FluentValidation;
using SmartSchool.Ioc.Behavior;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dominio.Alunos;
using SmartSchool.Dominio.Cursos;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Semestres;
using SmartSchool.Dados.Modulos.Alunos;
using SmartSchool.Dados.Modulos.Cursos;
using SmartSchool.Dados.Modulos.Disciplinas;
using SmartSchool.Dados.Modulos.Semestres;

[assembly: TestFramework("SmartSchool.Testes.API.BootStrapContainer", "SmartSchool.Testes.API")]
namespace SmartSchool.Testes.API
{
	public class BootStrapContainer : DependencyInjectionTestFramework
	{
		public readonly IConfiguration Configuration;
		public BootStrapContainer(IMessageSink messageSink) : base(messageSink)
		{
			Configuration = ConfiguracaoFabrica.Criar();

			var dataOpcoes = Configuration.GetSection("Data").Get<DataOpcoes>();
			AppSettings.SetarOpcoes(dataOpcoes);
		}
		protected override void ConfigureServices(IServiceCollection services)
		{
			var stringConexão = AppSettings.Data.DefaultConnectionString;

			services.AddTransient<SmartContexto>();

			services.AddDbContext<SmartContexto>(options =>
				options.UseSqlServer(stringConexão)
			);

			services.AddMediatR(typeof(ListarAlunosCommand).Assembly);
			services.AddMediatR(typeof(ListarAlunosHandler).Assembly);
			services.AddMediatR(typeof(ObterAlunoCommand).Assembly);
			services.AddMediatR(typeof(ObterAlunoHandler).Assembly);
			services.AddMediatR(typeof(AdicionarAlunoCommand).Assembly);
			services.AddMediatR(typeof(AdicionarAlunoHandler).Assembly);
			services.AddMediatR(typeof(AlterarAlunoCommand).Assembly);
			services.AddMediatR(typeof(AlterarAlunoHandler).Assembly);
			services.AddMediatR(typeof(ObterAlunoMatriculaHandler).Assembly);
			services.AddMediatR(typeof(ObterAlunoMatriculaCommand).Assembly);
			services.AddMediatR(typeof(ObterAlunoNomeCommand).Assembly);
			services.AddMediatR(typeof(ObterAlunoNomeHandler).Assembly);
			services.AddMediatR(typeof(ObterHistoricoAlunoCommand).Assembly);
			services.AddMediatR(typeof(ObterHistoricoAlunoHandler).Assembly);

			services.AddScoped<IRepositorioTask<Aluno>, AlunoRepositorioTask>();
			services.AddScoped<IRepositorioTask<Curso>, CursoRepositorioTask>();
			services.AddScoped<IRepositorioTask<Disciplina>, DisciplinaRepositorioTask>();
			services.AddScoped<IRepositorioTask<Semestre>, SemestreRepositorioTask>();

			services.AddValidatorsFromAssembly(typeof(ValidationBehavior<,>).Assembly);
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

			services.AddMemoryCache();

			#region Dados
			services.AddScoped<IUnidadeDeTrabalho, Contextos>();
			#endregion
		}
	}
}
