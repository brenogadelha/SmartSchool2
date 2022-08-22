using MediatR;
using SmartSchool.Dominio.Comum.Results;
using System;

namespace SmartSchool.Aplicacao.Alunos.RemoverAluno
{
    public class RemoverAlunoCommand : IRequest<IResult>
    {
        public Guid ID { get; set; }
    }
}
