using MediatR;
using SmartSchool.Dominio.Comum.Results;
using System;

namespace SmartSchool.Aplicacao.Alunos.ObterAlunoMatricula
{
    public class ObterAlunoMatriculaCommand : IRequest<IResult>
    {
        public int Matricula { get; set; }
    }
}
