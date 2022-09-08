using System;
using System.Collections.Generic;

namespace SmartSchool.Dto.Tccs
{
	public class TccDto
	{
		public string Tema { get; set; }
		public string Problematica { get; set; }
		public string Descricao { get; set; }
		public string Objetivo { get; set; }
		public List<Guid> Professores { get; set; }
	}
}
