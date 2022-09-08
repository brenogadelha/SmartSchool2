using MediatR;
using SmartSchool.Dominio.Comum.Results;
using System;
using System.Collections.Generic;

namespace SmartSchool.Aplicacao.Tccs.Adicionar
{
	public class AdicionarTccCommand : IRequest<IResult>
    {
		public string Tema { get; set; }
		public string Problematica { get; set; }
		public string Descricao { get; set; }
		public string Objetivo { get; set; }
		public List<Guid> Professores { get; set; }
	}
}
