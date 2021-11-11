using System;
using AutoMapper;

namespace SmartSchool.Testes.Compartilhado
{
    public class TesteIntegracaoBase : IDisposable
    {
        private static GerenciaBancoDeDados GerenciaBancoDeDados { get; set; }

        public TesteIntegracaoBase()
        {
            GerenciaBancoDeDados = new GerenciaBancoDeDados();
            GerenciaBancoDeDados.ExcluirBancoDeDados();
            GerenciaBancoDeDados.CriarBancoDeDados();
            GerenciaBancoDeDados.ExecutarMigrações();

            //Mapper.Initialize(cfg =>
            //{
            //    cfg.AddProfile(new PerfilDtoParaDominio());
            //    cfg.AddProfile(new PerfilDominioParaDto());
            //});
        }
        public void Dispose()
        {
            GerenciaBancoDeDados.Dispose();
            GerenciaBancoDeDados.ExcluirBancoDeDados();
        }
    }
}
