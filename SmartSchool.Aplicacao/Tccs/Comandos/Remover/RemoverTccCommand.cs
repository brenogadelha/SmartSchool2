using MediatR;
using SmartSchool.Dominio.Comum.Results;
using System;

namespace SmartSchool.Aplicacao.Tccs.Remover
{
	public class RemoverTccCommand : IRequest<IResult>
	{
		public Guid ID { get; set; }
	}
}
