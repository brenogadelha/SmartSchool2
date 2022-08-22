using SmartSchool.Comum.Dominio;
using SmartSchool.Comum.Dominio.Enums;
using SmartSchool.Dominio.Alunos;
using System;

namespace SmartSchool.Dominio.Tccs
{
	public class TccAlunoProfessor : IEntidade
	{
		private TccAlunoProfessor() { }

		public Guid TccID { get; private set; }
		public Aluno Aluno { get; private set; }
		public Guid ProfessorID { get; private set; }
		public Guid AlunoID { get; private set; }
		public TccProfessor ProfessorTcc { get; private set; }
		public DateTime DataSolicitacao { get; set; }
		public TccStatus Status { get; private set; }

		public static TccAlunoProfessor Criar(int periodo, Aluno aluno, TccProfessor professorTcc, TccStatus tccStatus)
		{
			var tccAlunoProfessor = new TccAlunoProfessor { ProfessorTcc = professorTcc, Aluno = aluno };
			tccAlunoProfessor.AlunoID = aluno.ID;
			tccAlunoProfessor.TccID = professorTcc.TccID;
			tccAlunoProfessor.ProfessorID = professorTcc.ProfessorID;
			tccAlunoProfessor.Status = tccStatus;
			tccAlunoProfessor.DataSolicitacao = DateTime.Now;

			return tccAlunoProfessor;
		}

		public static TccAlunoProfessor Criar(Guid tccId, Guid professorId, Guid alunoId, TccStatus tccStatus) => new TccAlunoProfessor()
		{
			TccID = tccId,
			ProfessorID = professorId,
			AlunoID = alunoId,
			Status = tccStatus,
			DataSolicitacao = DateTime.Now
		};
	}

}
