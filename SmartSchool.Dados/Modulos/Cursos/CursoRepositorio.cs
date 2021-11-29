using SmartSchool.Comum.Especificao;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dados.Comum;
using SmartSchool.Dominio.Cursos;
using SmartSchool.Dominio.Semestres;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartSchool.Dados.Modulos.Cursos
{
	public class CursoRepositorio : IRepositorio<Curso>
	{
		private readonly IUnidadeDeTrabalho _contexto;
		public CursoRepositorio(IUnidadeDeTrabalho contextos) => this._contexto = contextos;

		public void Adicionar(Curso entidade, bool finalizarTransacao = true)
		{
			this._contexto.SmartContexto.Cursos.Add(entidade);

			if (finalizarTransacao)
				this._contexto.SmartContexto.SaveChanges();
		}

		public void Atualizar(Curso entidade, bool finalizarTransacao = true)
		{
			this._contexto.SmartContexto.Cursos.Update(entidade);

			if (finalizarTransacao)
				this._contexto.SmartContexto.SaveChanges();
		}

		public IEnumerable<Curso> Obter() => this._contexto.SmartContexto.Cursos;

		public Curso Obter(IEspecificavel<Curso> especificacao) =>
			this._contexto.SmartContexto.ObterPorEspecificacao(especificacao).FirstOrDefault();

		public IEnumerable<Curso> Procurar(IEspecificavel<Curso> especificacao) =>
			this._contexto.SmartContexto.ObterPorEspecificacao(especificacao);

		public void Remover(Curso entidade, bool finalizarTransacao = true)
		{
			this._contexto.SmartContexto.Cursos.Remove(entidade);

			if (finalizarTransacao)
				this._contexto.SmartContexto.SaveChanges();
		}
	}
}
