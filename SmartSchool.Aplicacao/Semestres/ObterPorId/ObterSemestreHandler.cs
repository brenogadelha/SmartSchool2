using MediatR;
using SmartSchool.Comum.Mapeador;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Semestres.Servicos;
using SmartSchool.Dto.Semestres;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSchool.Aplicacao.Semestres.ObterPorId
{
    public class ObterSemestreHandler : IRequestHandler<ObterSemestreCommand, IResult>
    {
		private readonly ISemestreServicoDominio _semestreServicoDominio;

		public ObterSemestreHandler(ISemestreServicoDominio semestreServicoDominio)
        {
			this._semestreServicoDominio = semestreServicoDominio;
        }

        public async Task<IResult> Handle(ObterSemestreCommand request, CancellationToken cancellationToken)
        {
			var professor = await this._semestreServicoDominio.ObterAsync(request.Id);

			return Result<ObterSemestreDto>.Success(professor.MapearParaDto<ObterSemestreDto>());
        }
    }
}
