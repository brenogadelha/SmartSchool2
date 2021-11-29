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

            var dataOpcoes = configuracao.GetSection("Data").Get<DataOpcoes>();

            AppSettings.SetarOpcoes(dataOpcoes);
        }
    }
}