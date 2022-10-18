using MediatR;
using SmartSchool.Dominio.Comum.Results;
using System;

namespace SmartSchool.Aplicacao.Semestres.ObterPorId
{
    public class ObterSemestreQuery : IRequest<IResult>
    {
        public Guid Id { get; set; }
    }
}
