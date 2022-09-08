using SmartSchool.Comum.Especificao;
using System;
using System.Linq.Expressions;

namespace SmartSchool.Dominio.Tccs.Especificacao
{
	public class BuscaDeSolicitacaoTccPorAlunoIdEspecificacao : Especificacao<TccAlunoProfessor>
	{
		private readonly Guid _alunoId;
		public BuscaDeSolicitacaoTccPorAlunoIdEspecificacao(Guid alunoId)
		{
			this._alunoId = alunoId;
		}

		public override Expression<Func<TccAlunoProfessor, bool>> ExpressaoEspecificacao => x => x.AlunoID == this._alunoId;
	}
}
