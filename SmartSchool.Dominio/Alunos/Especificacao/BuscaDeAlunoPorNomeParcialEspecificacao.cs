using SmartSchool.Comum.Especificao;
using System;
using System.Linq.Expressions;

namespace SmartSchool.Dominio.Alunos.Especificacao
{
	public class BuscaDeAlunoPorNomeParcialEspecificacao : Especificacao<Aluno>
	{
		private readonly string _busca;

		public BuscaDeAlunoPorNomeParcialEspecificacao(string busca) => this._busca = busca;

		public override Expression<Func<Aluno, bool>> ExpressaoEspecificacao => x =>
			   (x.Nome.ToLower().Contains(_busca.ToLower()) ||
			   x.Sobrenome.ToLower().Contains(_busca.ToLower())) || (x.Nome.ToLower() +" "+ x.Sobrenome.ToLower()).Contains(_busca.ToLower()) &&
			   x.Ativo == true;
	}
}
