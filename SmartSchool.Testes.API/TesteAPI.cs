using Microsoft.Extensions.DependencyInjection;
using SmartSchool.Comum.Mapeador;
using SmartSchool.Ioc;
using SmartSchool.Testes.Compartilhado;
using System;
using Xunit;

namespace SmartSchool.Testes.API
{
	[CollectionDefinition(NAME)]
	public class ApiTestCollection : ICollectionFixture<TesteIntegracaoBase>
	{
		public const string NAME = "APIServiceCollection";
	}

	[Collection(ApiTestCollection.NAME)]
	public class TesteApi : GerenciaBancoDeDados
	{
		public TesteApi()
		{
			LimparBancoDeDados();
			Mapeador.SetMapper(ConfiguracaoAutoMap.Inicializar().CreateMapper());
		}

		protected ServiceProvider GetServiceProviderComMediatR(params (Type tipo, object instancia)[] servicesInjection)
		{
			var services = new ServiceCollection();

			foreach (var (tipo, instancia) in servicesInjection)
			{
				services.AddSingleton(tipo, instancia);
			}

			services.AddMyMediatR();

			return services.BuildServiceProvider();
		}
	}
}
