using SmartSchool.Comum.Especificao;
using System;
using System.Linq.Expressions;

namespace SmartSchool.Dominio.Alunos.Especificacao
{
	public class BuscaDeAlunoPorAtivoEspecificacao : Especificacao<Aluno>
	{
		private readonly bool _ativo;

		public BuscaDeAlunoPorAtivoEspecificacao(bool ativo) => this._ativo = ativo;

		public BuscaDeAlunoPorAtivoEspecificacao IncluiInformacoesDeDisciplina()
		{
			this.ObjetosInclusaoTipo.Add(x => x.AlunosDisciplinas);
			this.ObjetosInclusaoStrings.Add("AlunosDisciplinas.Disciplina");

			return this;
		}

		public BuscaDeAlunoPorAtivoEspecificacao IncluiInformacoesDeCurso()
		{
			this.ObjetosInclusaoTipo.Add(x => x.Curso);

			return this;
		}

		public override Expression<Func<Aluno, bool>> ExpressaoEspecificacao => x => x.Ativo == this._ativo;
	}
}
