using SmartSchool.Comum.Especificao;
using SmartSchool.Dominio.Professores;
using System;
using System.Linq.Expressions;

namespace SmartSchool.Dominio.Professores.Especificacao
{
	public class BuscaDeProfessorEspecificacao : Especificacao<Professor>
	{
		public override Expression<Func<Professor, bool>> ExpressaoEspecificacao => x => true;
	}
}
