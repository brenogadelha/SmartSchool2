using MediatR;
using SmartSchool.Aplicacao.Tccs.Adicionar;
using SmartSchool.Dominio.Comum.Results;
using System;
using System.Text.Json.Serialization;

namespace SmartSchool.Aplicacao.Tccs.Alterar
{
	public class AlterarTccCommand : AdicionarTccCommand, IRequest<IResult>
	{
		[JsonIgnore]
		public Guid ID { get; set; }
	}
}
