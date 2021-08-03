using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SmartSchool.Comum.Configuracao;
using SmartSchool.Comum.Infra.Opcoes;

namespace SmartSchool.Dados.Contextos
{
    public class SmartContextoBuilder : IDesignTimeDbContextFactory<SmartContexto>
    {
        public SmartContexto CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SmartContexto>();

            var stringConexão = ConfiguracaoFabrica.Criar().GetSection("Data").Get<DataOpcoes>().DefaultConnectionString;
            optionsBuilder.UseSqlServer(stringConexão);

            return new SmartContexto(optionsBuilder.Options);
        }
    }
}