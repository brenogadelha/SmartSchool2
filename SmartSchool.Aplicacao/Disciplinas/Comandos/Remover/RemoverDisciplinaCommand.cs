using MediatR;
using SmartSchool.Dominio.Comum.Results;
using System;

namespace SmartSchool.Aplicacao.Disciplinas.Remover
{
	public class RemoverDisciplinaCommand : IRequest<IResult>
	{
		public Guid ID { get; set; }
	}
}
