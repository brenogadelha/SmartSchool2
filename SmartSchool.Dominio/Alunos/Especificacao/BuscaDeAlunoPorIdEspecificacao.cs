using SmartSchool.Comum.Especificao;
using System;
using System.Linq.Expressions;

namespace SmartSchool.Dominio.Alunos.Especificacao
{
	public class BuscaDeAlunoPorIdEspecificacao : Especificacao<Aluno>
	{
		private readonly Guid _id;

		public BuscaDeAlunoPorIdEspecificacao(Guid id) => this._id = id;

		public BuscaDeAlunoPorIdEspecificacao IncluiInformacoesDeHistorico()
		{
			this.ObjetosInclusaoTipo.Add(x => x.SemestresDisciplinas);
			this.ObjetosInclusaoStrings.Add("SemestresDisciplinas");
			this.ObjetosInclusaoStrings.Add("SemestresDisciplinas.AlunoDisciplina.Disciplina");

			return this;
		}

		public BuscaDeAlunoPorIdEspecificacao IncluiInformacoesDeDisciplina()
		{
			this.ObjetosInclusaoTipo.Add(x => x.AlunosDisciplinas);
			this.ObjetosInclusaoStrings.Add("AlunosDisciplinas.Disciplina");

			return this;
		}

		public override Expression<Func<Aluno, bool>> ExpressaoEspecificacao => x => x.ID == this._id;
	}
}
