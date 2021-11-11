using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Comum.Configuracao;
using SmartSchool.Comum.Infra;
using SmartSchool.Comum.Infra.Opcoes;
using Xunit;
using Xunit.Abstractions;
using Xunit.DependencyInjection;

[assembly: TestFramework("SmartSchool.Testes.Integracao.BootStrapContainer", "SmartSchool.Testes.Integracao")]
namespace SmartSchool.Testes.Integracao
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
			services.AddMemoryCache();
			//return services.BuildServiceProvider();
		}
	}
}
