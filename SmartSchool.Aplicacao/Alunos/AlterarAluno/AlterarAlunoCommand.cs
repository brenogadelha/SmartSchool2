using MediatR;
using SmartSchool.Aplicacao.Alunos.AdicionarAluno;
using SmartSchool.Dominio.Comum.Results;
using System;
using System.Text.Json.Serialization;

namespace SmartSchool.Aplicacao.Alunos.AlterarAluno
{
	public class AlterarAlunoCommand : AdicionarAlunoCommand, IRequest<IResult>
	{
		[JsonIgnore]
		public Guid ID { get; set; }
		public bool Ativo { get; set; }
	}
}
