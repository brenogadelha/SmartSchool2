﻿using SmartSchool.Comum.Dominio.Enums;
using SmartSchool.Comum.Especificao;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SmartSchool.Dominio.Tccs.Especificacao
{
	public class BuscaDeTccPorAtivoEspecificacao : Especificacao<Tcc>
	{
		public BuscaDeTccPorAtivoEspecificacao IncluiInformacoesDeProfessores()
		{
			this.ObjetosInclusaoTipo.Add(x => x.TccProfessores);
			this.ObjetosInclusaoStrings.Add("TccProfessores.Professor");

			return this;
		}

		public override Expression<Func<Tcc, bool>> ExpressaoEspecificacao => x => x.Ativo == true && x.TccProfessores.Any(tp => tp.Professor.DisponibilidadeTcc == DisponibilidadeTcc.Disponível);
	}
}
