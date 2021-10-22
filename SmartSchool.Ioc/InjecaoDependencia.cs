﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Aplicacao.Alunos.Interface;
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
using SmartSchool.Dados.Modulos.Semestres;
using SmartSchool.Dados.Modulos.Usuarios;
using SmartSchool.Dominio.Alunos;
using SmartSchool.Dominio.Cursos;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Professores;
using SmartSchool.Dominio.Semestres;

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
			services.AddScoped<IProfessorServico, ProfessorServico>();
			services.AddScoped<IDisciplinaServico, DisciplinaServico>();
			services.AddScoped<ICursoServico, CursoServico>();
			services.AddScoped<ISemestreServico, SemestreServico>();

			#endregion
			#region Comum
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddSingleton<IMemoryCache, MemoryCache>();
			#endregion
		}
	}
}