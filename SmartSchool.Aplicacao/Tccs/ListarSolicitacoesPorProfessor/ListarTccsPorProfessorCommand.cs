using MediatR;
using SmartSchool.Comum.Dominio.Enums;
using SmartSchool.Dominio.Comum.Results;
using System;

namespace SmartSchool.Aplicacao.Tccs.ListarPorProfessor
{
    public class ListarTccsPorProfessorCommand : IRequest<IResult> 
    {
        public Guid ProfessorId { get; set; }
        public TccStatus StatusTcc { get; set; }
    }
}
