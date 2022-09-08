using MediatR;
using SmartSchool.Dominio.Comum.Results;
using System;

namespace SmartSchool.Aplicacao.Disciplinas.ObterPorId
{
    public class ObterDisciplinaCommand : IRequest<IResult>
    {
        public Guid Id { get; set; }
    }
}
