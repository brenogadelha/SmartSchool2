using MediatR;
using SmartSchool.Dominio.Comum.Results;
using System;

namespace SmartSchool.Aplicacao.Disciplinas.ObterProfessores
{
    public class ObterProfessoresDisciplinaQuery : IRequest<IResult>
    {
        public Guid Id { get; set; }
    }
}
