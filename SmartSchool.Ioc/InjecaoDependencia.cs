using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Parceiros.Template.Aplicacao.Pessoas.ListarPessoas;
using SmartSchool.Aplicacao.Alunos.AdicionarAluno;
using SmartSchool.Aplicacao.Alunos.AlterarAluno;
using SmartSchool.Aplicacao.Alunos.Interface;
using SmartSchool.Aplicacao.Alunos.ListarAlunos;
using SmartSchool.Aplicacao.Alunos.ObterAluno;
using SmartSchool.Aplicacao.Alunos.Servico;
using SmartSchool.Aplicacao.Cursos.Interface;
using SmartSchool.Aplicacao.Cursos.Servico;
using SmartSchool.Aplicacao.Disciplinas.Interface;
using SmartSchool.Aplicacao.Disciplinas.Servico;
using SmartSchool.Aplicacao.Professores.Interface;
using SmartSchool.Aplicacao.Professores.Servico;
using SmartSchool.Aplicacao.Semestres.Interface;
using SmartSchool.Aplicacao.Semestres.Servico;
using SmartSchool.Comum.Infra;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dados.Comum;
using SmartSchool.Dados.Contextos;
using SmartSchool.Dados.Modulos.Alunos;
using SmartSchool.Dados.Modulos.Cursos;
using SmartSchool.Dados.Modulos.Disciplinas;
using SmartSchool.Dados.Modulos.Semestres;
using SmartSchool.Dados.Modulos.Usuarios;
using SmartSchool.Dominio.Alunos;
using SmartSchool.Dominio.Alunos.Servicos;
using SmartSchool.Dominio.Cursos;
using SmartSchool.Dominio.Cursos.Servicos;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Disciplinas.Servicos;
using SmartSchool.Dominio.Professores;
using SmartSchool.Dominio.Semestres;
using SmartSchool.Dominio.Semestres.Servicos;
using SmartSchool.Ioc.Behavior;

namespace SmartSchool.Ioc
{
	public static class InjecaoDependencia
	{
		public static void Cadastrar(IServiceCollection services, IConfiguration configuracao)
		{
			services.AddConfiguracaoOpcoes(configuracao);

			var stringConexão = AppSettings.Data.DefaultConnectionString;

			services.AddDbContext<SmartContexto>(options =>
				options.UseSqlServer(stringConexão)
			);

			#region Repositorios
			services.AddScoped<IRepositorio<Aluno>, AlunoRepositorio>();
			services.AddScoped<IRepositorioTask<Aluno>, AlunoRepositorioTask>();
			services.AddScoped<IRepositorioTask<Curso>, CursoRepositorioTask>();
			services.AddScoped<IRepositorioTask<Disciplina>, DisciplinaRepositorioTask>();
			services.AddScoped<IRepositorioTask<Semestre>, SemestreRepositorioTask>();
			services.AddScoped<IRepositorio<Professor>, ProfessorRepositorio>();
			services.AddScoped<IRepositorio<Disciplina>, DisciplinaRepositorio>();
			services.AddScoped<IRepositorio<Curso>, CursoRepositorio>();
			services.AddScoped<IRepositorio<Semestre>, SemestreRepositorio>();

			#endregion

			#region Dados
			services.AddScoped<IUnidadeDeTrabalho, Contextos>();
			#endregion

			#region Aplicação
			services.AddScoped<IAlunoServico, AlunoServico>();
			services.AddScoped<IAlunoServicoDominio, AlunoServicoDominio>();
			services.AddScoped<IProfessorServico, ProfessorServico>();
			services.AddScoped<IDisciplinaServico, DisciplinaServico>();
			services.AddScoped<ICursoServico, CursoServico>();
			services.AddScoped<ICursoServicoDominio, CursoServicoDominio>();
			services.AddScoped<ISemestreServicoDominio, SemestreServicoDominio>();
			services.AddScoped<IDisciplinaServicoDominio, DisciplinaServicoDominio>();
			services.AddScoped<ISemestreServico, SemestreServico>();

			#endregion
		}

		public static IServiceCollection AddMyMediatR(this IServiceCollection services)
		{
			services.AddMediatR(typeof(ListarAlunosCommand).Assembly);
			services.AddMediatR(typeof(ListarAlunosHandler).Assembly);
			services.AddMediatR(typeof(ObterAlunoCommand).Assembly);
			services.AddMediatR(typeof(ObterAlunoHandler).Assembly);
			services.AddMediatR(typeof(AdicionarAlunoCommand).Assembly);
			services.AddMediatR(typeof(AdicionarAlunoHandler).Assembly);
			services.AddMediatR(typeof(AlterarAlunoCommand).Assembly);
			services.AddMediatR(typeof(AlterarAlunoHandler).Assembly);

			services.AddValidatorsFromAssembly(typeof(ValidationBehavior<,>).Assembly);
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

			return services;
		}
	}
}