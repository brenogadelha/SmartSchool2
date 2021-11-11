using SmartSchool.Comum.Especificao;
using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace SmartSchool.Dominio.Alunos.Especificacao
{
	public class BuscaDeAlunoPorCpfCnpjEspecificacao : Especificacao<Aluno>
	{
		private readonly string _cpf;

		public BuscaDeAlunoPorCpfCnpjEspecificacao(string cpf) => this._cpf = Regex.Replace(cpf, @"[^\d]", "");

		public override Expression<Func<Aluno, bool>> ExpressaoEspecificacao => x => (x.Cpf == this._cpf) && x.Ativo == true;
	}
}
