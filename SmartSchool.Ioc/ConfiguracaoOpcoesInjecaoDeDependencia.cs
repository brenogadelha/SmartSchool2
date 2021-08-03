using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Comum.Infra;
using SmartSchool.Comum.Infra.Opcoes;

namespace SmartSchool.Ioc
{
    internal static class ConfiguracaoOpcoesInjecaoDeDependencia
    {
        public static void AddConfiguracaoOpcoes(this IServiceCollection servicos, IConfiguration configuracao)
        {
            servicos.Configure<DataOpcoes>(options => configuracao.GetSection("Data").Bind(options));
            //servicos.Configure<LimitesContatoOpcoes>(options => configuracao.GetSection("LimitesContato").Bind(options));
            //servicos.Configure<EnderecosExternosOpcoes>(options => configuracao.GetSection("EnderecosExternos").Bind(options));
            //servicos.Configure<EnderecosExternosParceirosOpcoes>(options => configuracao.GetSection("EnderecosExternos:Parceiros").Bind(options));
            //servicos.Configure<EnderecosExternosNotificacaoOpcoes>(options => configuracao.GetSection("EnderecosExternos:Notificacao").Bind(options));
            //servicos.Configure<EnderecosExternosProdutoOpcoes>(options => configuracao.GetSection("EnderecosExternos:Produto").Bind(options));
            //servicos.Configure<SegurancaOpcoes>(options => configuracao.GetSection("Seguranca").Bind(options));
            //servicos.Configure<AzureOpcoes>(options => configuracao.GetSection("Azure").Bind(options));
            //servicos.Configure<SistemaOpcoes>(options => configuracao.GetSection("Sistema").Bind(options));
            //servicos.Configure<IntervalosDeChamadaOpcoes>(options => configuracao.GetSection("IntervalosDeChamada").Bind(options));
            //servicos.Configure<ExtensoesUploadArquivoOpcoes>(options => configuracao.GetSection("ExtensoesUploadArquivo").Bind(options));
            //servicos.Configure<AmbienteExatoOpcoes>(options => configuracao.GetSection("AmbienteExato").Bind(options));
            //servicos.Configure<UrlOpcoes>(options => configuracao.GetSection("UrlBase").Bind(options));
            //servicos.Configure<ClientIdLegadoOpcoes>(options => configuracao.GetSection("CriarClientIdLegado").Bind(options));
            //servicos.Configure<BarramentoOpcoes>(options => configuracao.GetSection("Barramento").Bind(options));

            var dataOpcoes = configuracao.GetSection("Data").Get<DataOpcoes>();
            //var limitesContatoOpcoes = configuracao.GetSection("LimitesContato").Get<LimitesContatoOpcoes>();
            //var enderecosExternosOpcoes = configuracao.GetSection("EnderecosExternos").Get<EnderecosExternosOpcoes>();
            //var enderecosExternosParceirosOpcoes = configuracao.GetSection("EnderecosExternos:Parceiros").Get<EnderecosExternosParceirosOpcoes>();
            //var enderecosExternosNotificacaoOpcoes = configuracao.GetSection("EnderecosExternos:Notificacao").Get<EnderecosExternosNotificacaoOpcoes>();
            //var enderecosExternosProdutoOpcoes = configuracao.GetSection("EnderecosExternos:Produto").Get<EnderecosExternosProdutoOpcoes>();
            //var segurancaOpcoes = configuracao.GetSection("Seguranca").Get<SegurancaOpcoes>();
            //var azureOpcoes = configuracao.GetSection("Azure").Get<AzureOpcoes>();
            //var sistemaOpcoes = configuracao.GetSection("Sistema").Get<SistemaOpcoes>();
            //var intervalosDeChamadaOpcoes = configuracao.GetSection("IntervalosDeChamada").Get<IntervalosDeChamadaOpcoes>();
            //var extensoesUploadArquivoOpcoes = configuracao.GetSection("ExtensoesUploadArquivo").Get<ExtensoesUploadArquivoOpcoes>();
            //var ambienteExatoOpcoes = configuracao.Get<AmbienteExatoOpcoes>();
            //var urlBaseOpcoes = configuracao.Get<UrlOpcoes>();
            //var criarClientIdLegadoOpcoes = configuracao.Get<ClientIdLegadoOpcoes>();
            //var barramentoOpcoes = configuracao.GetSection("Barramento").Get<BarramentoOpcoes>();

            AppSettings.SetarOpcoes(dataOpcoes);
        }
    }
}