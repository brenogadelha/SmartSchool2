using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SmartSchool.Comum.Dominio.Enums
{
	public enum StatusDisciplinaEnum
	{
		[Description("Cursando")]
		Cursando = 1,

		[Description("Aprovado")]
		Aprovado = 2,

		[Description("Reprovado")]
		Reprovado = 3
	}
}
