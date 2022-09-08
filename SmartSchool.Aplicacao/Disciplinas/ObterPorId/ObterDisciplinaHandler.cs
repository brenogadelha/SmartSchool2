using MediatR;
using SmartSchool.Comum.Mapeador;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Disciplinas.Servicos;
using SmartSchool.Dto.Disciplinas.Obter;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSchool.Aplicacao.Disciplinas.ObterPorId
{
    public class ObterDisciplinaHandler : IRequestHandler<ObterDisciplinaCommand, IResult>
    {
		private readonly IDisciplinaServicoDominio _disciplinaServicoDominio;

		public ObterDisciplinaHandler(IDisciplinaServicoDominio disciplinaServicoDominio)
        {
			this._disciplinaServicoDominio = disciplinaServicoDominio;
        }

        public async Task<IResult> Handle(ObterDisciplinaCommand request, CancellationToken cancellationToken)
        {
			var curso = await this._disciplinaServicoDominio.ObterAsync(request.Id);

			return Result<ObterDisciplinaDto>.Success(curso.MapearParaDto<ObterDisciplinaDto>());
        }
    }
}
