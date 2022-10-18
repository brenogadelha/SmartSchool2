using MediatR;
using SmartSchool.Aplicacao.Cursos.Adicionar;
using SmartSchool.Dominio.Comum.Results;
using System;
using System.Text.Json.Serialization;

namespace SmartSchool.Aplicacao.Cursos.Alterar
{
	public class AlterarCursoCommand : AdicionarCursoCommand, IRequest<IResult>
	{
		[JsonIgnore]
		public Guid ID { get; set; }
	}
}
