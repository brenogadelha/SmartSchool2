using MediatR;
using SmartSchool.Comum.Mapeador;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Tccs;
using SmartSchool.Dominio.Tccs.Especificacao;
using SmartSchool.Dto.Tccs;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSchool.Aplicacao.Tccs.ObterPorAluno
{
    public class ObterTccPorAlunoHandler : IRequestHandler<ObterTccPorAlunoCommand, IResult>
    {
        private readonly IRepositorio<TccAlunoProfessor> _tccRepositorio;

        public ObterTccPorAlunoHandler(IRepositorio<TccAlunoProfessor> tccRepositorio)
        {
            _tccRepositorio = tccRepositorio;
        }

        public async Task<IResult> Handle(ObterTccPorAlunoCommand request, CancellationToken cancellationToken)
        {
            var tcc = await this._tccRepositorio.ObterAsync(new BuscaDeSolicitacaoTccPorAlunoIdEspecificacao(request.AlunoId).IncluiInformacoesDeTcc().IncluiInformacoesDeProfessor());

            if (tcc == null)
                throw new RecursoInexistenteException("Não existe TCC para o aluno informado.");

            return Result<ObterStatusSolicitacaoTccDto>.Success(tcc.MapearParaDto<ObterStatusSolicitacaoTccDto>());
        }
    }
}
