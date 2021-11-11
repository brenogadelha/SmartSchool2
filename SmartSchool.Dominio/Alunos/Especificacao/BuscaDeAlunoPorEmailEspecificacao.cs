using SmartSchool.Comum.Especificao;
using System;
using System.Linq.Expressions;

namespace SmartSchool.Dominio.Alunos.Especificacao
{
	public class BuscaDeAlunoPorEmailEspecificacao : Especificacao<Aluno>
	{
		private readonly string _email;

		public BuscaDeAlunoPorEmailEspecificacao(string email) => this._email = email;

		public override Expression<Func<Aluno, bool>> ExpressaoEspecificacao => x => x.Email == this._email && x.Ativo == true;
	}
}
