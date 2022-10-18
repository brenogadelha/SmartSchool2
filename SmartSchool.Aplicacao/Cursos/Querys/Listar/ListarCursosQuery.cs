using MediatR;
using SmartSchool.Dominio.Comum.Results;

namespace SmartSchool.Aplicacao.Cursos.Listar
{
    public class ListarCursosQuery : IRequest<IResult> { }
}
