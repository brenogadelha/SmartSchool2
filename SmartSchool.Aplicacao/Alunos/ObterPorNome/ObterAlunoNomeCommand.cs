using MediatR;
using SmartSchool.Dominio.Comum.Results;
using System;

namespace SmartSchool.Aplicacao.Alunos.ObterPorNome
{
    public class ObterAlunoNomeCommand : IRequest<IResult>
    {
        public string Busca { get; set; }
    }
}
