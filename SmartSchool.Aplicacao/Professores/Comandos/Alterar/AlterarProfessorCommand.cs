using MediatR;
using SmartSchool.Aplicacao.Professores.Adicionar;
using SmartSchool.Dominio.Comum.Results;
using System;
using System.Text.Json.Serialization;

namespace SmartSchool.Aplicacao.Professores.Alterar
{
	public class AlterarProfessorCommand : AdicionarProfessorCommand, IRequest<IResult>
	{
		[JsonIgnore]
		public Guid ID { get; set; }
	}
}
