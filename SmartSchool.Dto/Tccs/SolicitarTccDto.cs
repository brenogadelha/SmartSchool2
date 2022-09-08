using System.Collections.Generic;
using System;

namespace SmartSchool.Dto.Tccs
{
	public class SolicitarTccDto
	{
		public string Solicitacao { get; set; }
		public List<Guid> AlunosIds { get; set; }
		public Guid ProfessorId { get; set; }
	}
}
