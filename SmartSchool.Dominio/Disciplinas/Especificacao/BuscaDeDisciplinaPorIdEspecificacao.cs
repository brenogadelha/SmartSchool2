﻿using SmartSchool.Comum.Especificao;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Professores;
using System;
using System.Linq.Expressions;

namespace SmartSchool.Dominio.Professores.Especificacao
{
	public class BuscaDeDisciplinaPorIdEspecificacao : Especificacao<Disciplina>
	{
		private readonly Guid _id;

		public BuscaDeDisciplinaPorIdEspecificacao(Guid id) => this._id = id;

		public override Expression<Func<Disciplina, bool>> ExpressaoEspecificacao => x => x.ID == this._id;
	}
}