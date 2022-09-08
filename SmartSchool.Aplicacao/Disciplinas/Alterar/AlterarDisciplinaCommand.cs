using MediatR;
using SmartSchool.Aplicacao.Disciplinas.Adicionar;
using SmartSchool.Dominio.Comum.Results;
using System;
using System.Text.Json.Serialization;

namespace SmartSchool.Aplicacao.Disciplinas.Alterar
{
	public class AlterarDisciplinaCommand : AdicionarDisciplinaCommand, IRequest<IResult>
	{
		[JsonIgnore]
		public Guid ID { get; set; }
	}
}
