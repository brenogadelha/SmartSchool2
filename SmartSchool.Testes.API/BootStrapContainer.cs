using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Aplicacao.Alunos.Adicionar;
using SmartSchool.Aplicacao.Alunos.Alterar;
using SmartSchool.Aplicacao.Alunos.Listar;
using SmartSchool.Aplicacao.Alunos.ObterHistorico;
using SmartSchool.Aplicacao.Alunos.ObterPorId;
using SmartSchool.Aplicacao.Alunos.ObterPorMatricula;
using SmartSchool.Aplicacao.Alunos.ObterPorNome;
using SmartSchool.Comum.Infra;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dados.Comum;
using SmartSchool.Dados.Contextos;
using SmartSchool.Dados.Modulos.Alunos;
using SmartSchool.Dados.Modulos.Cursos;
using SmartSchool.Dados.Modulos.Disciplinas;
using SmartSchool.Dados.Modulos.Professores;
using SmartSchool.Dados.Modulos.Semestres;
using SmartSchool.Dados.Modulos.Tccs;
using SmartSchool.Dominio.Alunos;
using SmartSchool.Dominio.Cursos;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Professores;
using SmartSchool.Dominio.Semestres;
using SmartSchool.Dominio.Tccs;
using SmartSchool.Ioc.Behavior;
using Xunit;
using Xunit.Abstractions;
using Xunit.DependencyInjection;

[assembly: TestFramework("SmartSchool.Testes.API.BootStrapContainer", "SmartSchool.Testes.API")]
namespace SmartSchool.Testes.API
{
	public class BootStrapContainer : DependencyInjectionTestFramework
	{
		public readonly IConfiguration Configuration;
		public BootStrapContainer(IMessageSink messageSink) : base(messageSink) { }
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

			services.AddScoped<IRepositorio<Aluno>, AlunoRepositorio>();
			services.AddScoped<IRepositorio<Curso>, CursoRepositorio>();
			services.AddScoped<IRepositorio<Disciplina>, DisciplinaRepositorio>();
			services.AddScoped<IRepositorio<Semestre>, SemestreRepositorio>();
			services.AddScoped<IRepositorio<Professor>, ProfessorRepositorio>();
			services.AddScoped<IRepositorio<Tcc>, TccRepositorio>();
			services.AddScoped<IRepositorio<TccAlunoProfessor>, TccAlunoProfessorRepositorio>();

			services.AddValidatorsFromAssembly(typeof(ValidationBehavior<,>).Assembly);
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

			services.AddMemoryCache();

			#region Dados
			services.AddScoped<IUnidadeDeTrabalho, Contextos>();
			#endregion
		}
	}
}
