using SmartSchool.Comum.Especificao;
using System;
using System.Linq.Expressions;

namespace SmartSchool.Dominio.Cursos.Especificacao
{
	public class BuscaDeCursoPorNomeEspecificacao : Especificacao<Curso>
	{
		private readonly string _nome;

		public BuscaDeCursoPorNomeEspecificacao(string nome) => this._nome = nome;

		public override Expression<Func<Curso, bool>> ExpressaoEspecificacao => x => x.Nome == this._nome;
	}
}
