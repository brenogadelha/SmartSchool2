using MediatR;
using SmartSchool.Dominio.Comum.Results;
using System;

namespace SmartSchool.Aplicacao.Professores.Remover
{
	public class RemoverProfessorCommand : IRequest<IResult>
	{
		public Guid ID { get; set; }
	}
}
