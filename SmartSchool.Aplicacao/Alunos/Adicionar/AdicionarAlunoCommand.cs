using MediatR;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dto.Alunos;
using System;
using System.Collections.Generic;

namespace SmartSchool.Aplicacao.Alunos.Adicionar
{
    public class AdicionarAlunoCommand : IRequest<IResult>
    {
		public int Matricula { get; set; }
		public string Nome { get; set; }
		public string Sobrenome { get; set; }
		public string Telefone { get; set; }
		public string Endereco { get; set; }
		public string Cpf { get; set; }
		public string Cidade { get; set; }
		public string Email { get; set; }
		public string Celular { get; set; }
		public DateTime DataNascimento { get; set; }
		public DateTime DataInicio { get; set; }
		public DateTime DataFim { get; set; }
		public List<AlunoDisciplinaDto> AlunosDisciplinas { get; set; } = new List<AlunoDisciplinaDto>();
		public Guid CursoId { get; set; }
	}
}
