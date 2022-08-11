using MediatR;
using SmartSchool.Dominio.Comum.Results;
using System;

namespace SmartSchool.Aplicacao.Alunos.ObterHistoricoAluno
{
    public class ObterHistoricoAlunoCommand : IRequest<IResult>
    {
        public Guid Id { get; set; }
        public int? Periodo { get; set; }
    }
}
