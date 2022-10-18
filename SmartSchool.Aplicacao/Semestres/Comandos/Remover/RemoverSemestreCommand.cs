using MediatR;
using SmartSchool.Dominio.Comum.Results;
using System;

namespace SmartSchool.Aplicacao.Semestres.Remover
{
	public class RemoverSemestreCommand : IRequest<IResult>
	{
		public Guid ID { get; set; }
	}
}
