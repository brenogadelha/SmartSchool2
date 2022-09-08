using MediatR;
using SmartSchool.Dominio.Comum.Results;
using System;

namespace SmartSchool.Aplicacao.Semestres.ObterPorId
{
    public class ObterSemestreCommand : IRequest<IResult>
    {
        public Guid Id { get; set; }
    }
}
