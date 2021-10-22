using SmartSchool.Comum.Especificao;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dados.Comum;
using SmartSchool.Dominio.Semestres;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartSchool.Dados.Modulos.Semestres
{
	public class SemestreRepositorio : IRepositorio<Semestre>
	{
		private readonly IUnidadeDeTrabalho _contexto;
		public SemestreRepositorio(IUnidadeDeTrabalho contextos) => this._contexto = contextos;

		public void Adicionar(Semestre entidade, bool finalizarTransacao = true)
		{
			this._contexto.SmartContexto.Semestres.Add(entidade);

			if (finalizarTransacao)
				this._contexto.SmartContexto.SaveChanges();
		}

		public void Atualizar(Semestre entidade, bool finalizarTransacao = true)
		{
			this._contexto.SmartContexto.Semestres.Update(entidade);

			if (finalizarTransacao)
				this._contexto.SmartContexto.SaveChanges();
		}

		public IEnumerable<Semestre> Obter() => this._contexto.SmartContexto.Semestres;

		public Semestre Obter(IEspecificavel<Semestre> especificacao) =>
			this._contexto.SmartContexto.ObterPorEspecificacao(especificacao).FirstOrDefault();

		public IEnumerable<Semestre> Procurar(IEspecificavel<Semestre> especificacao) =>
			this._contexto.SmartContexto.ObterPorEspecificacao(especificacao);

		public void Remover(Semestre entidade, bool finalizarTransacao = true) => this._contexto.SmartContexto.Semestres.Remove(entidade);

	}
}
