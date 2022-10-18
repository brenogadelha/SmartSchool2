using MediatR;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dto.Alunos;
using System;
using System.Collections.Generic;

namespace SmartSchool.Aplicacao.Cursos.Adicionar
{
    public class AdicionarCursoCommand : IRequest<IResult>
    {
		public string Nome { get; set; }
		public List<Guid> DisciplinasId { get; set; }
	}
}
