using MediatR;
using SmartSchool.Dominio.Comum.Results;
using System;

namespace SmartSchool.Aplicacao.Alunos.ObterHistorico
{
    public class ObterHistoricoAlunoQuery : IRequest<IResult>
    {
        public Guid Id { get; set; }
        public int? Periodo { get; set; }
    }
}
