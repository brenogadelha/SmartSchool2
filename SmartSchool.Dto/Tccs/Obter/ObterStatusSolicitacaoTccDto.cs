using System;

namespace SmartSchool.Dto.Tccs
{
	public class ObterStatusSolicitacaoTccDto
	{
		public string Tema { get; set; }
		public DateTime DataSolicitacao { get; set; }
		public string RespostaSolicitacao { get; private set; }
		public string NomeProfessor { get; private set; }
		public string EmailProfessor { get; private set; }
		public string Status { get; private set; }
	}
}
