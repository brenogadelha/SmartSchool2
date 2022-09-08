using MediatR;
using SmartSchool.Dominio.Comum.Results;
using System;

namespace SmartSchool.Aplicacao.Disciplinas.ObterProfessores
{
    public class ObterProfessoresDisciplinaCommand : IRequest<IResult>
    {
        public Guid Id { get; set; }
    }
}
