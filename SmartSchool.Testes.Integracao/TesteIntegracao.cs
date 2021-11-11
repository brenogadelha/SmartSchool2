using SmartSchool.Comum.Mapeador;
using SmartSchool.Ioc;
using SmartSchool.Testes.Compartilhado;
using Xunit;

namespace SmartSchool.Testes.Integracao
{
	[CollectionDefinition(NAME)]
	public class IntegrationTestCollection : ICollectionFixture<TesteIntegracaoBase>
	{
		public const string NAME = "IntegrationServiceCollection";
	}

	[Collection(IntegrationTestCollection.NAME)]
	public class TesteIntegracao : GerenciaBancoDeDados
	{
		public TesteIntegracao()
		{
			LimparBancoDeDados();

			Mapeador.SetMapper(ConfiguracaoAutoMap.Inicializar().CreateMapper());
		}
	}
}
