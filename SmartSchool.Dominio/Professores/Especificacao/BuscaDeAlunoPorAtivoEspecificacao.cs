using SmartSchool.Comum.Especificao;
using SmartSchool.Dominio.Professores;
using System;
using System.Linq.Expressions;

namespace SmartSchool.Dominio.Alunos.Especificacao
{
	public class BuscaDeProfessorEspecificacao : Especificacao<Professor>
	{
		private readonly bool _ativo;

		public BuscaDeProfessorEspecificacao(bool ativo) => this._ativo = ativo;

		public override Expression<Func<Professor, bool>> ExpressaoEspecificacao => x => true;
	}
}
