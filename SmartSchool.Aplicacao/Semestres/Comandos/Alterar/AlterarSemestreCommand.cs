using MediatR;
using SmartSchool.Aplicacao.Semestres.Adicionar;
using SmartSchool.Dominio.Comum.Results;
using System;
using System.Text.Json.Serialization;

namespace SmartSchool.Aplicacao.Semestres.Alterar
{
	public class AlterarSemestreCommand : AdicionarSemestreCommand, IRequest<IResult>
	{
		[JsonIgnore]
		public Guid ID { get; set; }
	}
}
