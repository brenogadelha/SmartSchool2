using SmartSchool.Comum.Dominio;
using SmartSchool.Comum.Dominio.Enums;
using SmartSchool.Dominio.Alunos;
using System;

namespace SmartSchool.Dominio.Tccs
{
	public class TccAlunoProfessor : IEntidade
	{
		public TccAlunoProfessor() { }

		public Guid TccID { get; private set; }
		public Aluno Aluno { get; private set; }
		public Guid ProfessorID { get; private set; }
		public Guid AlunoID { get; private set; }
		public TccProfessor ProfessorTcc { get; private set; }
		public DateTime DataSolicitacao { get; set; }
		public TccStatus Status { get; private set; }
		public string Solicitacao { get; set; }
		public string RespostaSolicitacao { get; set; }

		public static TccAlunoProfessor Criar(Aluno aluno, TccProfessor professorTcc, string solicitacao)
		{
			var tccAlunoProfessor = new TccAlunoProfessor { ProfessorTcc = professorTcc, Aluno = aluno };
			tccAlunoProfessor.AlunoID = aluno.ID;
			tccAlunoProfessor.TccID = professorTcc.TccID;
			tccAlunoProfessor.ProfessorID = professorTcc.ProfessorID;
			tccAlunoProfessor.Status = TccStatus.Solicitado;
			tccAlunoProfessor.DataSolicitacao = DateTime.Now;
			tccAlunoProfessor.Solicitacao = solicitacao;

			return tccAlunoProfessor;
		}

		public static TccAlunoProfessor Criar(Guid tccId, Guid professorId, Guid alunoId, string solicitacao) => new TccAlunoProfessor()
		{
			TccID = tccId,
			ProfessorID = professorId,
			AlunoID = alunoId,
			Status = TccStatus.Solicitado,
			DataSolicitacao = DateTime.Now,
			Solicitacao = solicitacao
		};

		public void AlterarStatus(TccStatus status) => this.Status = status;
		public void AlterarRespostaSolicitacao(string respostaSolicitacao) => this.RespostaSolicitacao = respostaSolicitacao;
	}

}
