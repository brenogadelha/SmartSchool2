using SmartSchool.Comum.Especificao;
using System;
using System.Linq.Expressions;

namespace SmartSchool.Dominio.Tccs.Especificacao
{
	public class BuscaDeTccPorIdEspecificacao : Especificacao<Tcc>
	{
		private readonly Guid _id;

		public BuscaDeTccPorIdEspecificacao(Guid id) => this._id = id;

		public BuscaDeTccPorIdEspecificacao IncluiInformacoesDeProfessores()
		{
			this.ObjetosInclusaoTipo.Add(x => x.TccProfessores);
			this.ObjetosInclusaoStrings.Add("TccProfessores.Professor");

			return this;
		}

		public override Expression<Func<Tcc, bool>> ExpressaoEspecificacao => x => x.ID == this._id;
	}
}
