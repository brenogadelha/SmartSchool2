﻿using SmartSchool.Comum.Dominio.Enums;
using SmartSchool.Comum.Especificao;
using System;
using System.Linq.Expressions;

namespace SmartSchool.Dominio.Tccs.Especificacao
{
	public class BuscaDeSolicitacaoTccPorProfessorIdEspecificacao : Especificacao<TccAlunoProfessor>
	{
		private readonly Guid _professorId;
		private readonly TccStatus? _tccStatus;

		public BuscaDeSolicitacaoTccPorProfessorIdEspecificacao(Guid professorId, TccStatus? tccStatus)
		{
			this._professorId = professorId;
			this._tccStatus = tccStatus;
		}

		//public BuscaDeSolicitacaoTccPorProfessorIdEspecificacao IncluiInformacoesDeProfessores()
		//{
		//	this.ObjetosInclusaoTipo.Add(x => x.TccProfessores);
		//	this.ObjetosInclusaoStrings.Add("TccProfessores.Professor");

		//	return this;
		//}

		public override Expression<Func<TccAlunoProfessor, bool>> ExpressaoEspecificacao => x => x.ProfessorID == this._professorId && this._tccStatus.HasValue ? x.Status == this._tccStatus : true;
	}
}
