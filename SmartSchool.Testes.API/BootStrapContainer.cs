using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;
using Xunit.DependencyInjection;
using SmartSchool.Comum.Infra.Opcoes;
using SmartSchool.Comum.Configuracao;
using SmartSchool.Comum.Infra;
using SmartSchool.Dados.Contextos;
using SmartSchool.Dados.Comum;

[assembly: TestFramework("SmartSchool.Testes.API.BootStrapContainer", "SmartSchool.Testes.API")]
namespace SmartSchool.Testes.API
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
			var stringConexão = AppSettings.Data.DefaultConnectionString;

			services.AddDbContext<SmartContexto>(options =>
				options.UseSqlServer(stringConexão)
			);

			services.AddMemoryCache();

			#region Dados
			services.AddScoped<IUnidadeDeTrabalho, Contextos>();
			#endregion
		}
	}
}
