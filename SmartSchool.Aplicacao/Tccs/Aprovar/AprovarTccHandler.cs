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
			var tcc = await this._tccRepositorio.ObterAsync(new BuscaDeSolicitacaoTccPorProfessorAlunoIdEspecificacao(request.ProfessorId, request.AlunoId));

			if (tcc == null)
				throw new RecursoInexistenteException("Não foi encontrado TCC para o Aluno e Professor informados.");

			if (request.StatusTcc == TccStatus.Negado && string.IsNullOrEmpty(request.RespostaSolicitacao))
				throw new ErroNegocioException("Em caso de negação, é necessário informar o motivo.");

			tcc.AlterarStatus(request.StatusTcc);
			tcc.AlterarRespostaSolicitacao(request.RespostaSolicitacao);

			// Caso tenha sido aprovado, inativamos o tema para que ninguém mais faça solicitação para o mesmo
			if (request.StatusTcc == TccStatus.Aceito)
				tcc.ProfessorTcc.Tcc.AlterarAtivo(false);

			await this._tccRepositorio.Atualizar(tcc);

			return Result.Success();
		}
	}
}
