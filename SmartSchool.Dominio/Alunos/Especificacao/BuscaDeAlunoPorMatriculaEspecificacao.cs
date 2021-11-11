using SmartSchool.Comum.Especificao;
using System;
using System.Linq.Expressions;

namespace SmartSchool.Dominio.Alunos.Especificacao
{
	public class BuscaDeAlunoPorMatriculaEspecificacao : Especificacao<Aluno>
	{
		private readonly int _matricula;

		public BuscaDeAlunoPorMatriculaEspecificacao(int matricula) => this._matricula = matricula;

		public BuscaDeAlunoPorMatriculaEspecificacao IncluiInformacoesDeHistorico()
		{
			this.ObjetosInclusaoTipo.Add(x => x.SemestresDisciplinas);
			this.ObjetosInclusaoStrings.Add("SemestresDisciplinas");
			this.ObjetosInclusaoStrings.Add("SemestresDisciplinas.AlunoDisciplina.Disciplina");

			return this;
		}

		public BuscaDeAlunoPorMatriculaEspecificacao IncluiInformacoesDeDisciplina()
		{
			this.ObjetosInclusaoTipo.Add(x => x.AlunosDisciplinas);
			this.ObjetosInclusaoStrings.Add("AlunosDisciplinas.Disciplina");

			return this;
		}

		public BuscaDeAlunoPorMatriculaEspecificacao IncluiInformacoesDeCurso()
		{
			this.ObjetosInclusaoTipo.Add(x => x.Curso);

			return this;
		}

		public override Expression<Func<Aluno, bool>> ExpressaoEspecificacao => x => x.Matricula == this._matricula;
	}
}
