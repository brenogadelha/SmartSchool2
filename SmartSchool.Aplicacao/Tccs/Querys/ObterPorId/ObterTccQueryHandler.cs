using MediatR;
using SmartSchool.Comum.Mapeador;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Tccs.Servicos;
using SmartSchool.Dto.Tccs;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSchool.Aplicacao.Tccs.ObterPorId
{
    public class ObterTccQueryHandler : IRequestHandler<ObterTccQuery, IResult>
    {
		private readonly ITccServicoDominio _tccServicoDominio;

		public ObterTccQueryHandler(ITccServicoDominio tccServicoDominio)
        {
			this._tccServicoDominio = tccServicoDominio;
        }

        public async Task<IResult> Handle(ObterTccQuery request, CancellationToken cancellationToken)
        {
			var tcc = await this._tccServicoDominio.ObterAsync(request.Id);

			return Result<ObterTccDto>.Success(tcc.MapearParaDto<ObterTccDto>());
        }
    }
}
