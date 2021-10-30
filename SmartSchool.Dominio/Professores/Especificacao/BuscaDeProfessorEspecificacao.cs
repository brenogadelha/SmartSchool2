using SmartSchool.Comum.Especificao;
using SmartSchool.Dominio.Professores;
using System;
using System.Linq.Expressions;

namespace SmartSchool.Dominio.Professores.Especificacao
{
	public class BuscaDeProfessorEspecificacao : Especificacao<Professor>
	{
		public BuscaDeProfessorEspecificacao IncluiInformacoesDeDisciplina()
		{
			this.ObjetosInclusaoTipo.Add(x => x.ProfessoresDisciplinas);
			this.ObjetosInclusaoStrings.Add("ProfessoresDisciplinas.Disciplina");

			return this;
		}

		public override Expression<Func<Professor, bool>> ExpressaoEspecificacao => x => true;
	}
}
