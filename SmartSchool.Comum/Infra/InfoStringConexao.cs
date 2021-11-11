using System.Data.SqlClient;

namespace SmartSchool.Comum.Infra
{
    public class InfoStringConexao

    {
        public string Servidor { get; set; }
        public string NomeBancoDeDados { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public bool SegurançaIntegrada { get; set; }
        public override string ToString()
        {
            return SegurançaIntegrada
                ? $"Server={Servidor};Database={NomeBancoDeDados};Trusted_Connection=True;MultipleActiveResultSets=true"
                : $"Server={Servidor};Database={NomeBancoDeDados};MultipleActiveResultSets=true;User={Login};Password={Senha};";
        }

        public InfoStringConexao NovoNomeDoBancoDeDados(string nomeBancoDeDados)
        {
            var copia = (InfoStringConexao)MemberwiseClone();
            copia.NomeBancoDeDados = nomeBancoDeDados;
            return copia;
        }

        public static InfoStringConexao Parse(string stringDeConexão)
        {
            var infoConexão = new InfoStringConexao();

            var construtor = new SqlConnectionStringBuilder(stringDeConexão);
            infoConexão.Servidor = construtor.DataSource;
            infoConexão.NomeBancoDeDados = construtor.InitialCatalog;
            if (construtor.IntegratedSecurity)
            {
                infoConexão.SegurançaIntegrada = true;
            }
            else
            {
                infoConexão.Login = construtor.UserID;
                infoConexão.Senha = construtor.Password;
            }
            return infoConexão;
        }
    }
}
