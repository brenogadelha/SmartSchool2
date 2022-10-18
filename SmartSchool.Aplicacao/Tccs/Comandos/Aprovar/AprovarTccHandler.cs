using MediatR;
using SmartSchool.Comum.Dominio.Enums;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Tccs;
using SmartSchool.Dominio.Tccs.Especificacao;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSchool.Aplicacao.Tccs.Aprovar
{
	public class AprovarTccHandler : IRequestHandler<AprovarTccCommand, IResult>
	{
		private readonly IRepositorio<TccAlunoProfessor> _tccRepositorio;

		public AprovarTccHandler(IRepositorio<TccAlunoProfessor> tccRepositorio)
		{
			this._tccRepositorio = tccRepositorio;
		}

		public async Task<IResult> Handle(AprovarTccCommand request, CancellationToken cancellationToken)
		{
			var solicitacaoTcc = await this._tccRepositorio.ObterAsync(new BuscaDeSolicitacaoTccPorProfessorAlunoIdEspecificacao(request.ProfessorId, request.AlunoId));

			if (solicitacaoTcc == null)
				throw new RecursoInexistenteException("Não foi encontrada solicitação de TCC para o Aluno informado.");

			if (request.StatusTcc == TccStatus.Negado && string.IsNullOrEmpty(request.RespostaSolicitacao))
				return Result.UnprocessableEntity("Em caso de negação, é necessário informar o motivo.");

			solicitacaoTcc.AlterarStatus(request.StatusTcc);
			solicitacaoTcc.AlterarRespostaSolicitacao(request.RespostaSolicitacao);

			await this._tccRepositorio.Atualizar(solicitacaoTcc);

			return Result.Success();
		}
	}
}
