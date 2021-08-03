using SmartSchool.Comum.Infra.Opcoes;
using System;

namespace SmartSchool.Comum.Infra
{
    public static class AppSettings
    {
        public static DataOpcoes Data { get; private set; }
       
        public static void SetarOpcoes(DataOpcoes dataOpcoes)
        {
            Data = dataOpcoes;            
        }

        //public static string ObterStringDeConexao(BancoDeDadosEnum bancoDeDados)
        //{
        //    string banco = string.Empty;
        //    switch (bancoDeDados)
        //    {
        //        case BancoDeDadosEnum.Web:
        //            banco = Data.WebConnectionString;
        //            break;
        //        case BancoDeDadosEnum.Previdencia:
        //            banco = Data.PrevidenciaConnectionString;
        //            break;
        //        case BancoDeDadosEnum.Capitalizacao:
        //            banco = Data.CapitalizacaoConnectionString;
        //            break;
        //        case BancoDeDadosEnum.Corretor:
        //            banco = Data.CorretorConnectionString;
        //            break;
        //        case BancoDeDadosEnum.LumisPortalIH:
        //            banco = Data.LumisPortalIHConnectionString;
        //            break;
        //        case BancoDeDadosEnum.ControleDeAcesso:
        //            banco = Data.PortalControleAcessoConnectionString;
        //            break;
        //        case BancoDeDadosEnum.PrevidenciaRG:
        //            banco = Data.PrevidenciaRGConnectionString;
        //            break;
        //        case BancoDeDadosEnum.LumisPortalIHRG:
        //            banco = Data.LumisPortalIHRGConnectionString;
        //            break;
        //        default:
        //            banco = Data.DefaultConnectionString;
        //            break;
        //    }

        //    string connectionString = banco;

        //    if (string.IsNullOrEmpty(connectionString))
        //        throw new ArgumentException($"Connection string para banco de dados '{banco}' não encontrada");

        //    return connectionString;
        //}

        //public static string ObterConfiguracaoBarramento(ConfiguracaoBarramentoEnum configuracaoBarramento)
        //{
        //    string configuracao = string.Empty;

        //    switch (configuracaoBarramento)
        //    {
        //        case ConfiguracaoBarramentoEnum.NumeroSerieCertificado:
        //            configuracao = Barramento.NumeroSerieCertificado;
        //            break;

        //        case ConfiguracaoBarramentoEnum.EnderecoBase:
        //            configuracao = Barramento.EnderecoBase;
        //            break;
        //    }

        //    return configuracao;
        //}

        //public static string ObterRecursoBarramento(RecursoBarramentoEnum recursoBarramento)
        //{
        //    string recurso = string.Empty;

        //    switch (recursoBarramento)
        //    {
        //        case RecursoBarramentoEnum.Profissoes:
        //            recurso = Barramento.RecursoProfissoes;
        //            break;
        //    }

        //    return recurso;
        //}
    }
}
