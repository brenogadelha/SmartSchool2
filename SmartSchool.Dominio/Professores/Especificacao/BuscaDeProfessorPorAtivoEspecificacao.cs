using SmartSchool.Comum.Especificao;
using System;
using System.Linq.Expressions;

namespace SmartSchool.Dominio.Professores.Especificacao
{
	public class BuscaDeProfessorPorAtivoEspecificacao : Especificacao<Professor>
	{
		public BuscaDeProfessorPorAtivoEspecificacao IncluiInformacoesDeDisciplina()
		{
			this.ObjetosInclusaoTipo.Add(x => x.ProfessoresDisciplinas);
			this.ObjetosInclusaoStrings.Add("ProfessoresDisciplinas.Disciplina");

			return this;
		}

		public override Expression<Func<Professor, bool>> ExpressaoEspecificacao => x => x.Ativo == true;
	}
}
