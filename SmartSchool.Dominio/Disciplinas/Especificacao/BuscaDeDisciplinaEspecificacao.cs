using SmartSchool.Comum.Especificao;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Professores;
using System;
using System.Linq.Expressions;

namespace SmartSchool.Dominio.Disciplinas.Especificacao
{
	public class BuscaDeDisciplinaEspecificacao : Especificacao<Disciplina>
	{
		public BuscaDeDisciplinaEspecificacao IncluiInformacoesDeProfessor()
		{
			this.ObjetosInclusaoTipo.Add(x => x.ProfessoresDisciplinas);
			this.ObjetosInclusaoStrings.Add("ProfessoresDisciplinas.Professor");

			return this;
		}

		public override Expression<Func<Disciplina, bool>> ExpressaoEspecificacao => x => true;
	}
}
