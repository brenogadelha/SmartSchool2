using MediatR;
using SmartSchool.Dominio.Comum.Results;
using System;
using System.Collections.Generic;

namespace SmartSchool.Aplicacao.Tccs.Solicitar
{
	public class SolicitarTccCommand : IRequest<IResult>
	{
		public Guid TccId { get; set; }
		public Guid ProfessorId { get; set; }
		public List<Guid> AlunosIds { get; set; }
		public string Solicitacao { get; set; }
	}
}
