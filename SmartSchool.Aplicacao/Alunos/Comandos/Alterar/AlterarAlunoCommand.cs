using MediatR;
using SmartSchool.Aplicacao.Alunos.Adicionar;
using SmartSchool.Dominio.Comum.Results;
using System;
using System.Text.Json.Serialization;

namespace SmartSchool.Aplicacao.Alunos.Alterar
{
	public class AlterarAlunoCommand : AdicionarAlunoCommand, IRequest<IResult>
	{
		[JsonIgnore]
		public Guid ID { get; set; }
		public bool Ativo { get; set; }
	}
}
