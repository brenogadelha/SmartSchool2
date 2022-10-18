using SmartSchool.Comum.Especificao;
using System;
using System.Linq.Expressions;

namespace SmartSchool.Dominio.Disciplinas.Especificacao
{
	public class BuscaDeDisciplinaPorAtivoEspecificacao : Especificacao<Disciplina>
	{
		public BuscaDeDisciplinaPorAtivoEspecificacao IncluiInformacoesDeProfessor()
		{
			this.ObjetosInclusaoTipo.Add(x => x.ProfessoresDisciplinas);
			this.ObjetosInclusaoStrings.Add("ProfessoresDisciplinas.Professor");

			return this;
		}

		public override Expression<Func<Disciplina, bool>> ExpressaoEspecificacao => x => x.Ativo == true;
	}
}
