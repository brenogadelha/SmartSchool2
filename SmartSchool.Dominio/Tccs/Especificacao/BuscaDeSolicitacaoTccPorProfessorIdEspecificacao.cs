using SmartSchool.Comum.Dominio.Enums;
using SmartSchool.Comum.Especificao;
using System;
using System.Linq.Expressions;

namespace SmartSchool.Dominio.Tccs.Especificacao
{
	public class BuscaDeSolicitacaoTccPorProfessorIdEspecificacao : Especificacao<TccAlunoProfessor>
	{
		private readonly Guid _professorId;
		private readonly TccStatus _tccStatus;

		public BuscaDeSolicitacaoTccPorProfessorIdEspecificacao(Guid professorId, TccStatus tccStatus)
		{
			this._professorId = professorId;
			this._tccStatus = tccStatus;
		}

		public BuscaDeSolicitacaoTccPorProfessorIdEspecificacao IncluiInformacoesDeTcc()
		{
			this.ObjetosInclusaoTipo.Add(x => x.ProfessorTcc.Tcc);

			return this;
		}

		public BuscaDeSolicitacaoTccPorProfessorIdEspecificacao IncluiInformacoesDeAluno()
		{
			this.ObjetosInclusaoTipo.Add(x => x.Aluno);

			return this;
		}

		public override Expression<Func<TccAlunoProfessor, bool>> ExpressaoEspecificacao => x => x.ProfessorID == this._professorId && this._tccStatus > 0 ? x.Status == this._tccStatus : true;
	}
}
