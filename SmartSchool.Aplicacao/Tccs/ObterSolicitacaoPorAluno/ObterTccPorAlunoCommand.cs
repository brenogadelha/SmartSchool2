using MediatR;
using SmartSchool.Comum.Dominio.Enums;
using SmartSchool.Dominio.Comum.Results;
using System;

namespace SmartSchool.Aplicacao.Tccs.ObterPorAluno
{
    public class ObterTccPorAlunoCommand : IRequest<IResult> 
    {
        public Guid AlunoId { get; set; }
    }
}
