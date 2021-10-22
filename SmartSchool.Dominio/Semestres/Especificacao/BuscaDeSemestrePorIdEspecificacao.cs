using SmartSchool.Comum.Especificao;
using System;
using System.Linq.Expressions;

namespace SmartSchool.Dominio.Semestres.Especificacao
{
	public class BuscaDeSemestrePorIdEspecificacao : Especificacao<Semestre>
	{
		private readonly Guid _id;

		public BuscaDeSemestrePorIdEspecificacao(Guid id) => this._id = id;

		public override Expression<Func<Semestre, bool>> ExpressaoEspecificacao => x => x.ID == this._id;
	}
}
