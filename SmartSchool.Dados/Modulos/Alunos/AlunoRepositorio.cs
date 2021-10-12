using SmartSchool.Comum.Especificao;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dados.Comum;
using SmartSchool.Dominio.Alunos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartSchool.Dados.Modulos.Alunos
{
	public class AlunoRepositorio : IRepositorio<Aluno>
	{
		private readonly IUnidadeDeTrabalho _contexto;
		public AlunoRepositorio(IUnidadeDeTrabalho contextos) => this._contexto = contextos;

		public void Adicionar(Aluno entidade, bool finalizarTransacao = true)
		{
			this._contexto.SmartContexto.Alunos.Add(entidade);

			if (finalizarTransacao)
				this._contexto.SmartContexto.SaveChanges();
		}

		public void Atualizar(Aluno entidade, bool finalizarTransacao = true)
		{
			this._contexto.SmartContexto.Alunos.Update(entidade);

			if (finalizarTransacao)
				this._contexto.SmartContexto.SaveChanges();
		}

		public IEnumerable<Aluno> Obter() => this._contexto.SmartContexto.Alunos;

		public Aluno Obter(IEspecificavel<Aluno> especificacao) =>
			this._contexto.SmartContexto.ObterPorEspecificacao(especificacao).FirstOrDefault();

		public IEnumerable<Aluno> Procurar(IEspecificavel<Aluno> especificacao) =>
			this._contexto.SmartContexto.ObterPorEspecificacao(especificacao);

		public void Remover(Aluno entidade, bool finalizarTransacao = true) => throw new NotImplementedException();

	}
}
