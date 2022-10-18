using SmartSchool.Comum.Especificao;
using System;
using System.Linq.Expressions;

namespace SmartSchool.Dominio.Semestres.Especificacao
{
	public class BuscaDeSemestrePorAtivoEspecificacao : Especificacao<Semestre>
	{
		public override Expression<Func<Semestre, bool>> ExpressaoEspecificacao => x => x.Ativo == true;
	}
}
