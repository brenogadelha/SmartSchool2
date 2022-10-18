using MediatR;
using SmartSchool.Comum.Mapeador;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Cursos.Servicos;
using SmartSchool.Dto.Curso;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSchool.Aplicacao.Cursos.ObterPorId
{
    public class ObterCursoQueryHandler : IRequestHandler<ObterCursoQuery, IResult>
    {
		private readonly ICursoServicoDominio _cursoServicoDominio;

		public ObterCursoQueryHandler(ICursoServicoDominio cursoServicoDominio)
        {
			this._cursoServicoDominio = cursoServicoDominio;
        }

        public async Task<IResult> Handle(ObterCursoQuery request, CancellationToken cancellationToken)
        {
			var curso = await this._cursoServicoDominio.ObterAsync(request.Id);

			return Result<ObterCursoDto>.Success(curso.MapearParaDto<ObterCursoDto>());
        }
    }
}
