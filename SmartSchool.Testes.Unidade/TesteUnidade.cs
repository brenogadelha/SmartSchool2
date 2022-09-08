using Microsoft.Extensions.Configuration;
using SmartSchool.Comum.Configuracao;
using SmartSchool.Comum.Mapeador;
using SmartSchool.Ioc;
using SmartSchool.Testes.Compartilhado;
using Xunit;

namespace SmartSchool.Testes.Unidade
{
    [Collection(UnitTestCollection.Name)]
    public class TesteUnidade : BaseMediatorServiceProvider { }

    [CollectionDefinition(Name)]
    public class UnitTestCollection : ICollectionFixture<TesteUnidadeBase>
    {
        public const string Name = "UnitServiceCollection";
    }

    public class TesteUnidadeBase 
	{
        public readonly IConfiguration Configuration;
        public TesteUnidadeBase()
        {
            Configuration = ConfiguracaoFabrica.Criar();            

            //AppSettings.SetarOpcoes(dataOpcoes);

            Mapeador.SetMapper(ConfiguracaoAutoMap.Inicializar().CreateMapper());

            //Mapper.Initialize(cfg =>
            //{
            //    cfg.AddProfile(new PerfilDtoParaDominio());
            //    cfg.AddProfile(new PerfilDominioParaDto());
            //});
        }
    }
}
