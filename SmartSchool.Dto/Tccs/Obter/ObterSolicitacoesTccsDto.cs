using SmartSchool.Comum.Dominio.Enums;
using System;

namespace SmartSchool.Dto.Tccs
{
	public class ObterSolicitacoesTccsDto
	{
		public Guid TccID { get; private set; }
		public Guid ProfessorID { get; private set; }
		public Guid AlunoID { get; private set; }
		public string Tema { get; set; }
		public DateTime DataSolicitacao { get; set; }
		public string Status { get; private set; }
		public string NomeAluno { get; private set; }
		public string EmailAluno { get; private set; }
		public int MatriculaAluno { get; private set; }
		public string Solicitacao { get; private set; }
	}
}
