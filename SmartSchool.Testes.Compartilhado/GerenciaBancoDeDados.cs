using Microsoft.EntityFrameworkCore;
using SmartSchool.Comum.Infra;
using SmartSchool.Dados.Contextos;
using SmartSchool.Testes.Compartilhado.ScriptsEstaticos;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace SmartSchool.Testes.Compartilhado
{
    public class GerenciaBancoDeDados : IDisposable
    {
        private static string StringDeConexão;
        protected SmartContexto SmartContexto;
        private readonly InfoStringConexao InfoStringConexao;
        private static string NomeBancoDeDados;

        static GerenciaBancoDeDados()
        {
            StringDeConexão = AppSettings.Data.DefaultConnectionString;
            GerenciaBancoDeDados.NomeBancoDeDados = InfoStringConexao.Parse(StringDeConexão).NomeBancoDeDados;
        }

        public GerenciaBancoDeDados()
        {
            var connectionString = AppSettings.Data.DefaultConnectionString;
            this.InfoStringConexao = InfoStringConexao.Parse(connectionString);
            var optionsBuilder = new DbContextOptionsBuilder<SmartContexto>();
            optionsBuilder.UseSqlServer(connectionString);

            this.SmartContexto = new SmartContexto(optionsBuilder.Options);
        }

        public void Dispose()
        {
            this.SmartContexto.Dispose();
            LimparBancoDeDados();
            GC.SuppressFinalize(this);
        }

        public static void LimparBancoDeDados()
        {
            NoCheckConstraints(StringDeConexão);
            ApagarRegistros(StringDeConexão);
            VerificarConstraints(StringDeConexão);
            CriarTabelasAuxiliares(StringDeConexão);
        }

        public void ExecutarMigrações()
        {
            Environment.SetEnvironmentVariable("Teste", "Teste");
            this.SmartContexto.Database.Migrate();
            Environment.SetEnvironmentVariable("Teste", string.Empty);
        }


        public void ExcluirBancoDeDados()
        {
            using (var conn = new SqlConnection(this.InfoStringConexao.NovoNomeDoBancoDeDados("master").ToString()))
            {
                string dbFileName, dbLogFileName;
                conn.Open();

                using (var cmdFileName = conn.CreateCommand())
                {
                    cmdFileName.CommandText =
                        $"SELECT physical_name FROM sys.master_files WHERE name = '{this.InfoStringConexao.NomeBancoDeDados}'";
                    dbFileName = (string)cmdFileName.ExecuteScalar();

                    cmdFileName.CommandText =
                        $"SELECT physical_name FROM sys.master_files WHERE name = '{this.InfoStringConexao.NomeBancoDeDados}_log'";
                    dbLogFileName = (string)cmdFileName.ExecuteScalar();
                }

                if (!string.IsNullOrWhiteSpace(dbFileName))
                {
                    using (var cmd = conn.CreateCommand())
                    {
                        const string command =
                            "ALTER DATABASE [{0}] SET OFFLINE WITH ROLLBACK IMMEDIATE; DROP database [{0}];";
                        cmd.CommandText = string.Format(command, this.InfoStringConexao.NomeBancoDeDados);
                        cmd.ExecuteNonQuery();
                    }

                    File.Delete(dbFileName);
                    File.Delete(dbLogFileName);
                }
            }
        }

        public void CriarBancoDeDados()
        {
            using (var conn = new SqlConnection(this.InfoStringConexao.NovoNomeDoBancoDeDados("master").ToString()))
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText =
                        $"CREATE database [{this.InfoStringConexao.NomeBancoDeDados}] COLLATE SQL_Latin1_General_CP1_CI_AI;";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void CriarTabelasAuxiliares(string connectionString)
        {
            ExecutarSql(TabelasAuxiliares.ObterScriptLog(NomeBancoDeDados), connectionString);
        }

        public static string ObterPathMSSQL()
        {
            var sqlVersions = new[] { 13, 14 };
            const string sqlDataFolder = @"Program Files\Microsoft SQL Server\MSSQL{0}.MSSQLSERVER\MSSQL\DATA";

            var drives = DriveInfo.GetDrives();
            var appDataFolderPath = drives.SelectMany(d => sqlVersions.Select(v => $"{d.Name}{string.Format(sqlDataFolder, v)}"))
                                          .First(Directory.Exists);

            if (string.IsNullOrWhiteSpace(appDataFolderPath))
                throw new DirectoryNotFoundException($"{sqlDataFolder} não existe!");
            return appDataFolderPath;
        }

        public static void ExecutarSql(string comandoSql, string stringDeConexão)
        {
            using (var connection = new SqlConnection(stringDeConexão))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = comandoSql;
                    command.ExecuteNonQuery();
                }
            }
        }

        private static void NoCheckConstraints(string connectionString) =>
            ExecutarSql("exec sp_MSforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'", connectionString);

        private static void ApagarRegistros(string connectionString)
        {
            var tabelasQueNãoDevemSerLimpas = new[] { "__EFMigrationsHistory" };
            var condicao = string.Join(" AND ", tabelasQueNãoDevemSerLimpas.Select(x => $"''?'' <> ''[dbo].[{x}]''"));
            var innerCommand = $@"
               IF {condicao}
                       DELETE FROM ?
           ";
            var proc = $"exec sp_MSforeachtable '{innerCommand}'";
            ExecutarSql(proc, connectionString);
        }

        private static void VerificarConstraints(string connectionString) =>
            ExecutarSql("exec sp_MSforeachtable 'ALTER TABLE ? CHECK CONSTRAINT all'", connectionString);
    }
}
