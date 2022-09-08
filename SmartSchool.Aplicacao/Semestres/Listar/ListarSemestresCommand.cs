using MediatR;
using SmartSchool.Dominio.Comum.Results;

namespace SmartSchool.Aplicacao.Semestres.Listar
{
    public class ListarSemestresCommand : IRequest<IResult> { }
}
