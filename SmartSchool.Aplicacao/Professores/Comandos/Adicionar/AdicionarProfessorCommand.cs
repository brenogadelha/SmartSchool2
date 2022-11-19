using MediatR;
using SmartSchool.Dominio.Comum.Results;
using System.Collections.Generic;
using System;

namespace SmartSchool.Aplicacao.Professores.Adicionar
{
	public class AdicionarProfessorCommand : IRequest<IResult>
    {
		public int Matricula { get; set; }
		public string Nome { get; set; }
		public string Email { get; set; }
		public List<Guid> Disciplinas { get; set; }
	}
}
