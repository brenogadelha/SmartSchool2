using System;
using System.Collections.Generic;

namespace SmartSchool.Dto.Tccs
{
	public class TccDto
	{
		public string Tema { get; set; }
		public string Descricao { get; set; }
		public List<Guid> Professores { get; set; }
	}
}
