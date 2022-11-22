using MediatR;
using SmartSchool.Dominio.Comum.Results;
using System;

namespace SmartSchool.Aplicacao.Tccs.Desvincular
{
	public class DesvincularTccCommand : IRequest<IResult>
	{
		public Guid ID { get; set; }
	}
}
