using MediatR;
using SmartSchool.Comum.Dominio.Enums;
using SmartSchool.Dominio.Comum.Results;
using System;
using System.Text.Json.Serialization;

namespace SmartSchool.Aplicacao.Professores.Alterar
{
	public class AlterarDisponibilidadeTccProfessorCommand : IRequest<IResult>
	{
		[JsonIgnore]
		public Guid ID { get; set; }
		public DisponibilidadeTcc DisponibilidadeTcc { get; set; }
	}
}
