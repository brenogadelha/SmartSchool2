using System.ComponentModel;

namespace SmartSchool.Comum.Dominio.Enums
{
	public enum TccStatus
	{
		[Description("Solicitado")]
		Solicitado = 1,

		[Description("Aceito")]
		Aceito = 2,

		[Description("Negado")]
		Negado = 3
	}
}
