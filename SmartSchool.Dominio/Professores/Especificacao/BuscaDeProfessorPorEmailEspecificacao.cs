using SmartSchool.Comum.Especificao;
using System;
using System.Linq.Expressions;

namespace SmartSchool.Dominio.Professores.Especificacao
{
	public class BuscaDeProfessorPorEmailEspecificacao : Especificacao<Professor>
	{
		private readonly string _email;

		public BuscaDeProfessorPorEmailEspecificacao(string email) => this._email = email;

		public override Expression<Func<Professor, bool>> ExpressaoEspecificacao => x => x.Email == this._email && x.Ativo == true;
	}
}
