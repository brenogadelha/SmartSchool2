using SmartSchool.Comum.Especificao;
using SmartSchool.Dominio.Disciplinas;
using System;
using System.Linq.Expressions;

namespace SmartSchool.Dominio.Disciplinas.Especificacao
{
	public class BuscaDeDisciplinaPorNomeEspecificacao : Especificacao<Disciplina>
	{
		private readonly string _nome;

		public BuscaDeDisciplinaPorNomeEspecificacao(string nome) => this._nome = nome;

		public override Expression<Func<Disciplina, bool>> ExpressaoEspecificacao => x => x.Nome == this._nome && x.Ativo == true;
	}
}
