using MediatR;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Tccs;
using SmartSchool.Dominio.Tccs.Servicos;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSchool.Aplicacao.Tccs.Remover
{
	public class RemoverTccHandler : IRequestHandler<RemoverTccCommand, IResult>
	{
		private readonly IRepositorio<Tcc> _tccRepositorio;
		private readonly ITccServicoDominio _tccServicoDominio;

		public RemoverTccHandler(IRepositorio<Tcc> tccRepositorio, ITccServicoDominio tccServicoDominio)
		{
			this._tccRepositorio = tccRepositorio;
			this._tccServicoDominio = tccServicoDominio;
		}

		public async Task<IResult> Handle(RemoverTccCommand request, CancellationToken cancellationToken)
		{
			var tcc = await this._tccServicoDominio.ObterAsync(request.ID);

			tcc.AlterarAtivo(false);

			await this._tccRepositorio.Atualizar(tcc, true);

			return Result.Success();
		}
	}
}
