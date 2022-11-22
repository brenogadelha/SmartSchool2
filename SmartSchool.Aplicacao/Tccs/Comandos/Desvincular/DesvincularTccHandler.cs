using MediatR;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Tccs;
using SmartSchool.Dominio.Tccs.Especificacao;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSchool.Aplicacao.Tccs.Desvincular
{
	public class DesvincularTccHandler : IRequestHandler<DesvincularTccCommand, IResult>
	{
		private readonly IRepositorio<TccAlunoProfessor> _tccAlunoProfessorRepositorio;

		public DesvincularTccHandler(IRepositorio<TccAlunoProfessor> tccAlunoProfessorRepositorio)
		{
			this._tccAlunoProfessorRepositorio = tccAlunoProfessorRepositorio;
		}

		public async Task<IResult> Handle(DesvincularTccCommand request, CancellationToken cancellationToken)
		{
			var tccAlunoProfessor = await this._tccAlunoProfessorRepositorio.ObterAsync(new BuscaDeSolicitacaoTccPorAlunoIdEspecificacao(request.ID));

			await this._tccAlunoProfessorRepositorio.RemoverAsync(tccAlunoProfessor, true);

			return Result.Success();
		}
	}
}
