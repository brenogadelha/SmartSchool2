using System.Text;

namespace SmartSchool.Testes.Compartilhado.ScriptsEstaticos
{
	public static class TabelasAuxiliares
	{
		public static string ObterScriptLog(string nomeDoBanco)
		{
			var builder = new StringBuilder();

			builder.Append($"USE [{nomeDoBanco}] ");
			builder.Append("IF (EXISTS (SELECT *");
			builder.Append("                 FROM INFORMATION_SCHEMA.TABLES ");
			builder.Append("                 WHERE TABLE_SCHEMA = 'dbo'");
			builder.Append("                 AND  TABLE_NAME = 'LogException')) ");
			builder.Append("BEGIN ");
			builder.Append("    DROP TABLE LogException; ");
			builder.Append("END ");

			builder.Append("CREATE TABLE [dbo].[LogException]( ");
			builder.Append("	[CodLog] [int] IDENTITY(1,1) NOT NULL, ");
			builder.Append("	[UserUniqueId] [uniqueidentifier] NOT NULL, ");
			builder.Append("	[DtaLog] [datetime] NOT NULL, ");
			builder.Append("	[DscURL] [varchar](300) NOT NULL, ");
			builder.Append("	[DscMensagem] [varchar](max) NULL, ");
			builder.Append("	[DscStackTrace] [varchar](max) NULL, ");
			builder.Append("	[Matricula] [char](10) NULL, ");
			builder.Append("	[ClientInfo] [text] NULL, ");
			builder.Append(" CONSTRAINT [PK_LogException] PRIMARY KEY CLUSTERED ");
			builder.Append("( ");
			builder.Append("	[CodLog] ASC ");
			builder.Append(")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ");
			builder.Append(") ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] ");

			return builder.ToString();
		}
	}
}
