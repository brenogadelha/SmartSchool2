using MediatR;
using SmartSchool.Comum.Dominio.Enums;
using SmartSchool.Dominio.Comum.Results;
using System;

namespace SmartSchool.Aplicacao.Tccs.Aprovar
{
    public class AprovarTccCommand : IRequest<IResult> 
    {
        public Guid ProfessorId { get; set; }
        public Guid AlunoId { get; set; }
        public TccStatus StatusTcc { get; set; }
        public string RespostaSolicitacao { get; set; }
    }
}
