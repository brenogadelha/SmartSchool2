using MediatR;
using SmartSchool.Dominio.Comum.Results;
using System;

namespace SmartSchool.Aplicacao.Alunos.ObterAluno
{
    public class ObterAlunoCommand : IRequest<IResult>
    {
        public Guid Id { get; set; }
    }
}
