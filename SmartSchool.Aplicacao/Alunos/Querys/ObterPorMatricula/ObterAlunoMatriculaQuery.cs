using MediatR;
using SmartSchool.Dominio.Comum.Results;
using System;

namespace SmartSchool.Aplicacao.Alunos.ObterPorMatricula
{
    public class ObterAlunoMatriculaQuery : IRequest<IResult>
    {
        public int Matricula { get; set; }
    }
}
