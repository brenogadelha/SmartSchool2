using SmartSchool.Comum.Dominio;
using System;

namespace SmartSchool.Dominio.TCCS
{
	public class tcc : IEntidadeAgregavel<Guid>
	{
		public Guid ID { get; private set; }
		public string Tema { get; set; }
	}
}


