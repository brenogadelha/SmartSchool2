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
using SmartSchool.Aplicacao.Alunos.RemoverAluno;
using SmartSchool.Aplicacao.Disciplinas.Alterar;
using SmartSchool.Aplicacao.Professores.Adicionar;
using SmartSchool.Aplicacao.Professores.Listar;
using SmartSchool.Aplicacao.Professores.ObterPorId;
using SmartSchool.Aplicacao.Professores.Remover;
using SmartSchool.Aplicacao.Tccs.Adicionar;
using SmartSchool.Aplicacao.Tccs.Alterar;
using SmartSchool.Aplicacao.Tccs.Aprovar;
using SmartSchool.Aplicacao.Tccs.Listar;
using SmartSchool.Aplicacao.Tccs.ListarPorProfessor;
using SmartSchool.Aplicacao.Tccs.ObterPorAluno;
using SmartSchool.Aplicacao.Tccs.ObterPorId;
using SmartSchool.Aplicacao.Tccs.Remover;
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
using SmartSchool.Dominio.Alunos.Servicos;
using SmartSchool.Dominio.Cursos;
using SmartSchool.Dominio.Cursos.Servicos;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Disciplinas.Servicos;
using SmartSchool.Dominio.Professores;
using SmartSchool.Dominio.Professores.Servicos;
using SmartSchool.Dominio.Semestres;
using SmartSchool.Dominio.Semestres.Servicos;
using SmartSchool.Dominio.Tccs;
using SmartSchool.Dominio.Tccs.Servicos;
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
			services.AddScoped<IRepositorio<Curso>, CursoRepositorio>();
			services.AddScoped<IRepositorio<Disciplina>, DisciplinaRepositorio>();
			services.AddScoped<IRepositorio<Semestre>, SemestreRepositorio>();
			services.AddScoped<IRepositorio<Professor>, ProfessorRepositorio>();
			services.AddScoped<IRepositorio<Tcc>, TccRepositorio>();
			services.AddScoped<IRepositorio<TccAlunoProfessor>, TccAlunoProfessorRepositorio>();

			#endregion

			#region Dados
			services.AddScoped<IUnidadeDeTrabalho, Contextos>();
			#endregion

			#region Aplicação
			services.AddScoped<IAlunoServicoDominio, AlunoServicoDominio>();
			services.AddScoped<IProfessorServicoDominio, ProfessorServicoDominio>();
			services.AddScoped<ICursoServicoDominio, CursoServicoDominio>();
			services.AddScoped<ISemestreServicoDominio, SemestreServicoDominio>();
			services.AddScoped<IDisciplinaServicoDominio, DisciplinaServicoDominio>();
			services.AddScoped<ITccServicoDominio, TccServicoDominio>();

			#endregion
		}

		public static IServiceCollection AddMyMediatR(this IServiceCollection services)
		{
			services.AddMediatR(typeof(ValidationBehavior<,>).Assembly);

			services.AddMediatR(typeof(ListarAlunosQueryHandler).Assembly);
			services.AddMediatR(typeof(ObterAlunoQueryHandler).Assembly);
			services.AddMediatR(typeof(AdicionarAlunoHandler).Assembly);
			services.AddMediatR(typeof(AlterarAlunoHandler).Assembly);
			services.AddMediatR(typeof(ObterAlunoMatriculaQueryHandler).Assembly);
			services.AddMediatR(typeof(ObterAlunoNomeQueryHandler).Assembly);
			services.AddMediatR(typeof(ObterHistoricoAlunoQueryHandler).Assembly);
			services.AddMediatR(typeof(RemoverAlunoHandler).Assembly);

			//services.AddMediatR(typeof(ListarAlunosCommand).Assembly);
			//services.AddMediatR(typeof(ObterAlunoCommand).Assembly);
			//services.AddMediatR(typeof(AdicionarAlunoCommand).Assembly);
			//services.AddMediatR(typeof(AlterarAlunoCommand).Assembly);
			//services.AddMediatR(typeof(ObterAlunoMatriculaCommand).Assembly);
			//services.AddMediatR(typeof(ObterAlunoNomeCommand).Assembly);
			//services.AddMediatR(typeof(ObterHistoricoAlunoCommand).Assembly);
			//services.AddMediatR(typeof(RemoverAlunoCommand).Assembly);

			services.AddMediatR(typeof(AdicionarProfessorHandler).Assembly);
			services.AddMediatR(typeof(ObterProfessorQueryHandler).Assembly);
			services.AddMediatR(typeof(ListarProfessoresQueryHandler).Assembly);
			services.AddMediatR(typeof(AlterarProfessorHandler).Assembly);
			services.AddMediatR(typeof(RemoverProfessorHandler).Assembly);
			services.AddMediatR(typeof(AlterarDisponibilidadeTccProfessorHandler).Assembly);

			services.AddMediatR(typeof(AdicionarTccHandler).Assembly);
			services.AddMediatR(typeof(ObterTccQueryHandler).Assembly);
			services.AddMediatR(typeof(ListarTccsQueryHandler).Assembly);
			services.AddMediatR(typeof(AlterarTccHandler).Assembly);
			services.AddMediatR(typeof(AprovarTccHandler).Assembly);
			services.AddMediatR(typeof(ListarTccsPorProfessorQueryHandler).Assembly);
			services.AddMediatR(typeof(ObterTccPorAlunoQueryHandler).Assembly);
			services.AddMediatR(typeof(RemoverTccHandler).Assembly);

			//services.AddMediatR(typeof(ListarProfessoresCommand).Assembly);
			//services.AddMediatR(typeof(ObterProfessorCommand).Assembly);
			//services.AddMediatR(typeof(AdicionarProfessorCommand).Assembly);
			//services.AddMediatR(typeof(AlterarProfessorCommand).Assembly);
			//services.AddMediatR(typeof(RemoverProfessorCommand).Assembly);

			services.AddValidatorsFromAssembly(typeof(ValidationBehavior<,>).Assembly);
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

			return services;
		}
	}
}