using MediatR;
using SmartSchool.Dominio.Comum.Results;
using System;

namespace SmartSchool.Aplicacao.Tccs.ObterPorAluno
{
    public class ObterTccPorAlunoQuery : IRequest<IResult> 
    {
        public Guid AlunoId { get; set; }
    }
}
