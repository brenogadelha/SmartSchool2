using MediatR;
using SmartSchool.Dominio.Comum.Results;
using System;

namespace SmartSchool.Aplicacao.Tccs.ObterPorId
{
    public class ObterTccQuery : IRequest<IResult>
    {
        public Guid Id { get; set; }
    }
}
