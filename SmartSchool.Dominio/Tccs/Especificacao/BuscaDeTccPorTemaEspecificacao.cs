using SmartSchool.Comum.Especificao;
using System;
using System.Linq.Expressions;

namespace SmartSchool.Dominio.Tccs.Especificacao
{
	public class BuscaDeTccPorTemaEspecificacao : Especificacao<Tcc>
	{
		private readonly string _tema;

		public BuscaDeTccPorTemaEspecificacao(string tema) => this._tema = tema;

		public override Expression<Func<Tcc, bool>> ExpressaoEspecificacao => x => x.Tema.ToLower() == this._tema.ToLower();
	}
}
