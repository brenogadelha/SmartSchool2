using SmartSchool.Comum.Dominio.Enums;
using SmartSchool.Comum.Especificao;
using System;
using System.Linq.Expressions;

namespace SmartSchool.Dominio.Tccs.Especificacao
{
	public class BuscaDeSolicitacaoTccPorProfessorAlunoIdEspecificacao : Especificacao<TccAlunoProfessor>
	{
		private readonly Guid _professorId;
		private readonly Guid _alunoId;

		public BuscaDeSolicitacaoTccPorProfessorAlunoIdEspecificacao(Guid professorId, Guid alunoId)
		{
			this._professorId = professorId;
			this._alunoId = alunoId;
		}

		//public BuscaDeSolicitacaoTccPorProfessorIdEspecificacao IncluiInformacoesDeProfessores()
		//{
		//	this.ObjetosInclusaoTipo.Add(x => x.TccProfessores);
		//	this.ObjetosInclusaoStrings.Add("TccProfessores.Professor");

		//	return this;
		//}

		public override Expression<Func<TccAlunoProfessor, bool>> ExpressaoEspecificacao => x => x.AlunoID == this._alunoId && x.ProfessorID == this._professorId && x.Status == TccStatus.Solicitado;
	}
}
