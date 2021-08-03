﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Aplicacao.Alunos.Interface;
using SmartSchool.Aplicacao.Alunos.Servico;
using SmartSchool.Comum.Infra;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dados.Comum;
using SmartSchool.Dados.Contextos;
using SmartSchool.Dados.Modulos.Usuarios;
using SmartSchool.Dominio.Alunos;
using System.Collections.Generic;

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

            #endregion

            #region Dados
            services.AddScoped<IUnidadeDeTrabalho, Contextos>();
            #endregion

            #region Aplicação
            services.AddScoped<IAlunoServico, AlunoServico>();

            #endregion
            #region Comum
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IMemoryCache, MemoryCache>();
            #endregion
        }
    }
}