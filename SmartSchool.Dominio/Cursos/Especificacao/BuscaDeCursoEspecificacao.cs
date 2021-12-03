using SmartSchool.Comum.Especificao;
using System;
using System.Linq.Expressions;

namespace SmartSchool.Dominio.Cursos.Especificacao
{
	public class BuscaDeCursoEspecificacao : Especificacao<Curso>
	{
		public BuscaDeCursoEspecificacao IncluiInformacoesDeDisciplina()
		{
			this.ObjetosInclusaoTipo.Add(x => x.CursosDisciplinas);
			this.ObjetosInclusaoStrings.Add("CursosDisciplinas.Disciplina");

			return this;
		}

		public override Expression<Func<Curso, bool>> ExpressaoEspecificacao => x => true;
	}
}
