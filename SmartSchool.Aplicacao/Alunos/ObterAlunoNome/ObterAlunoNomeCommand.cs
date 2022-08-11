using MediatR;
using SmartSchool.Dominio.Comum.Results;
using System;

namespace SmartSchool.Aplicacao.Alunos.ObterAlunoNome
{
    public class ObterAlunoNomeCommand : IRequest<IResult>
    {
        public string Busca { get; set; }
    }
}
