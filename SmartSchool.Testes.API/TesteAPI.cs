using SmartSchool.Comum.Mapeador;
using SmartSchool.Ioc;
using SmartSchool.Testes.Compartilhado;
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
	}
}
