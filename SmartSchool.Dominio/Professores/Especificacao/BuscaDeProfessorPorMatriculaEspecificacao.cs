using SmartSchool.Comum.Especificao;
using System;
using System.Linq.Expressions;

namespace SmartSchool.Dominio.Professores.Especificacao
{
	public class BuscaDeProfessorPorMatriculaEspecificacao : Especificacao<Professor>
	{
		private readonly int _matricula;

		public BuscaDeProfessorPorMatriculaEspecificacao(int matricula) => this._matricula = matricula;

		public override Expression<Func<Professor, bool>> ExpressaoEspecificacao => x => x.Matricula == this._matricula && x.Ativo == true;
	}
}
