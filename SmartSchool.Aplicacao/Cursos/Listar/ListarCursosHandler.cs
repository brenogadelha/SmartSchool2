using MediatR;
using SmartSchool.Comum.Mapeador;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Cursos;
using SmartSchool.Dominio.Cursos.Especificacao;
using SmartSchool.Dto.Curso;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSchool.Aplicacao.Cursos.Listar
{
    public class ListarCursosHandler : IRequestHandler<ListarCursosCommand, IResult>
    {
        private readonly IRepositorio<Curso> _cursoRepositorio;

        public ListarCursosHandler(IRepositorio<Curso> cursoRepositorio)
        {
            _cursoRepositorio = cursoRepositorio;
        }

        public async Task<IResult> Handle(ListarCursosCommand request, CancellationToken cancellationToken)
        {
            var cursos = await this._cursoRepositorio.Procurar(new BuscaDeCursoPorAtivoEspecificacao().IncluiInformacoesDeDisciplina());

            return Result<IEnumerable<ObterCursoDto>>.Success(cursos.MapearParaDto<ObterCursoDto>());
        }
    }
}
