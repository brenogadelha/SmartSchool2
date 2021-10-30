using SmartSchool.Comum.Especificao;
using System;
using System.Linq.Expressions;

namespace SmartSchool.Dominio.Professores.Especificacao
{
	public class BuscaDeProfessorPorIdEspecificacao : Especificacao<Professor>
	{
		private readonly Guid _id;

		public BuscaDeProfessorPorIdEspecificacao(Guid id) => this._id = id;

		public BuscaDeProfessorPorIdEspecificacao IncluiInformacoesDeDisciplina()
		{
			this.ObjetosInclusaoTipo.Add(x => x.ProfessoresDisciplinas);
			this.ObjetosInclusaoStrings.Add("ProfessoresDisciplinas.Disciplina");

			return this;
		}

		public override Expression<Func<Professor, bool>> ExpressaoEspecificacao => x => x.ID == this._id;
	}
}
