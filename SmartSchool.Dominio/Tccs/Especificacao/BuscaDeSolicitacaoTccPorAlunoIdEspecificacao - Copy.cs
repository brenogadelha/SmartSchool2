using SmartSchool.Comum.Especificao;
using System;
using System.Linq.Expressions;

namespace SmartSchool.Dominio.Tccs.Especificacao
{
	public class BuscaDeSolicitacaoTccPorIdEspecificacao : Especificacao<TccAlunoProfessor>
	{
		private readonly Guid _tccId;

		public BuscaDeSolicitacaoTccPorIdEspecificacao(Guid tccId)
		{
			this._tccId = tccId;
		}

		public override Expression<Func<TccAlunoProfessor, bool>> ExpressaoEspecificacao => x => x.TccID == this._tccId;
	}
}
