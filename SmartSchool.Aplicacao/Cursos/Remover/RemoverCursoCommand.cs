using MediatR;
using SmartSchool.Dominio.Comum.Results;
using System;

namespace SmartSchool.Aplicacao.Cursos.Remover
{
	public class RemoverCursoCommand : IRequest<IResult>
	{
		public Guid ID { get; set; }
	}
}
