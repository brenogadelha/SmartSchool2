using MediatR;
using SmartSchool.Dominio.Comum.Results;
using System;

namespace SmartSchool.Aplicacao.Alunos.ObterPorMatricula
{
    public class ObterAlunoMatriculaCommand : IRequest<IResult>
    {
        public int Matricula { get; set; }
    }
}
