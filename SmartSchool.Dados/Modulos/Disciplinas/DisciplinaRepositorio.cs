using SmartSchool.Comum.Especificao;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dados.Comum;
using SmartSchool.Dominio.Disciplinas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartSchool.Dados.Modulos.Usuarios
{
	public class DisciplinaRepositorio : IRepositorio<Disciplina>
	{
		private readonly IUnidadeDeTrabalho _contexto;
		public DisciplinaRepositorio(IUnidadeDeTrabalho contextos) => this._contexto = contextos;

		public void Adicionar(Disciplina entidade, bool finalizarTransacao = true)
		{
			this._contexto.SmartContexto.Disciplinas.Add(entidade);

			if (finalizarTransacao)
				this._contexto.SmartContexto.SaveChanges();
		}

		public void Atualizar(Disciplina entidade, bool finalizarTransacao = true)
		{
			this._contexto.SmartContexto.Disciplinas.Update(entidade);

			if (finalizarTransacao)
				this._contexto.SmartContexto.SaveChanges();
		}

		public IEnumerable<Disciplina> Obter() => this._contexto.SmartContexto.Disciplinas;

		public Disciplina Obter(IEspecificavel<Disciplina> especificacao) =>
			this._contexto.SmartContexto.ObterPorEspecificacao(especificacao).FirstOrDefault();

		public IEnumerable<Disciplina> Procurar(IEspecificavel<Disciplina> especificacao) =>
			this._contexto.SmartContexto.ObterPorEspecificacao(especificacao);

		public void Remover(Disciplina entidade, bool finalizarTransacao = true) => throw new NotImplementedException();

	}
}
