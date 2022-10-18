using MediatR;
using SmartSchool.Dominio.Comum.Results;
using System;

namespace SmartSchool.Aplicacao.Disciplinas.ObterPorId
{
    public class ObterDisciplinaQuery : IRequest<IResult>
    {
        public Guid Id { get; set; }
    }
}
