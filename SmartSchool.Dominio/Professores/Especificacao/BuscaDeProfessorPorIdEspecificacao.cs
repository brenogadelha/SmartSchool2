using SmartSchool.Comum.Especificao;
using SmartSchool.Dominio.Professores;
using System;
using System.Linq.Expressions;

namespace SmartSchool.Dominio.Professores.Especificacao
{
	public class BuscaDeProfessorPorIdEspecificacao : Especificacao<Professor>
	{
		private readonly Guid _id;

		public BuscaDeProfessorPorIdEspecificacao(Guid id) => this._id = id;

		public override Expression<Func<Professor, bool>> ExpressaoEspecificacao => x => x.ID == this._id;
	}
}
