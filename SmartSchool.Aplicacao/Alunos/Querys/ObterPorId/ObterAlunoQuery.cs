using MediatR;
using SmartSchool.Dominio.Comum.Results;
using System;

namespace SmartSchool.Aplicacao.Alunos.ObterPorId
{
    public class ObterAlunoQuery : IRequest<IResult>
    {
        public Guid Id { get; set; }
    }
}
