using SmartSchool.Comum.Especificao;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dados.Comum;
using SmartSchool.Dominio.Alunos;
using SmartSchool.Dominio.Professores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartSchool.Dados.Modulos.Usuarios
{
	public class ProfessorRepositorio : IRepositorio<Professor>
	{
		private readonly IUnidadeDeTrabalho _contexto;
		public ProfessorRepositorio(IUnidadeDeTrabalho contextos) => this._contexto = contextos;

		public void Adicionar(Professor entidade, bool finalizarTransacao = true)
		{
			this._contexto.SmartContexto.Professores.Add(entidade);

			if (finalizarTransacao)
				this._contexto.SmartContexto.SaveChanges();
		}

		public void Atualizar(Professor entidade, bool finalizarTransacao = true)
		{
			this._contexto.SmartContexto.Professores.Update(entidade);

			if (finalizarTransacao)
				this._contexto.SmartContexto.SaveChanges();
		}

		public IEnumerable<Professor> Obter() => this._contexto.SmartContexto.Professores;

		public Professor Obter(IEspecificavel<Professor> especificacao) =>
			this._contexto.SmartContexto.ObterPorEspecificacao(especificacao).FirstOrDefault();

		public IEnumerable<Professor> Procurar(IEspecificavel<Professor> especificacao) =>
			this._contexto.SmartContexto.ObterPorEspecificacao(especificacao);

		public void Remover(Professor entidade, bool finalizarTransacao = true)
		{
			this._contexto.SmartContexto.Professores.Remove(entidade);

			if (finalizarTransacao)
				this._contexto.SmartContexto.SaveChanges();
		}

	}
}
