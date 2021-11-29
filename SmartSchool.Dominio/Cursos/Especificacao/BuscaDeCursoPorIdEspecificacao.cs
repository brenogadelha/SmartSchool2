using SmartSchool.Comum.Especificao;
using System;
using System.Linq.Expressions;

namespace SmartSchool.Dominio.Cursos.Especificacao
{
	public class BuscaDeCursoPorIdEspecificacao : Especificacao<Curso>
	{
		private readonly Guid _id;

		public BuscaDeCursoPorIdEspecificacao(Guid id) => this._id = id;

		public BuscaDeCursoPorIdEspecificacao IncluiInformacoesDeDisciplina()
		{
			this.ObjetosInclusaoTipo.Add(x => x.CursosDisciplinas);
			this.ObjetosInclusaoStrings.Add("CursosDisciplinas.Disciplina");

			return this;
		}

		public override Expression<Func<Curso, bool>> ExpressaoEspecificacao => x => x.ID == this._id;
	}
}
