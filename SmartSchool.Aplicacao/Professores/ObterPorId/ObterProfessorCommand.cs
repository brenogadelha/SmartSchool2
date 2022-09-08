using MediatR;
using SmartSchool.Dominio.Comum.Results;
using System;

namespace SmartSchool.Aplicacao.Professores.ObterPorId
{
    public class ObterProfessorCommand : IRequest<IResult>
    {
        public Guid Id { get; set; }
    }
}
