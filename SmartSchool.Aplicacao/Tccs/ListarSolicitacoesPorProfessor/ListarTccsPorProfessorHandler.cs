using MediatR;
using SmartSchool.Comum.Mapeador;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Tccs;
using SmartSchool.Dominio.Tccs.Especificacao;
using SmartSchool.Dto.Tccs;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSchool.Aplicacao.Tccs.ListarPorProfessor
{
    public class ListarTccsPorProfessorHandler : IRequestHandler<ListarTccsPorProfessorCommand, IResult>
    {
        private readonly IRepositorio<TccAlunoProfessor> _tccRepositorio;

        public ListarTccsPorProfessorHandler(IRepositorio<TccAlunoProfessor> tccRepositorio)
        {
            _tccRepositorio = tccRepositorio;
        }

        public async Task<IResult> Handle(ListarTccsPorProfessorCommand request, CancellationToken cancellationToken)
        {
            var tccs = await this._tccRepositorio.Procurar(new BuscaDeSolicitacaoTccPorProfessorIdEspecificacao(request.ProfessorId, request.StatusTcc));

            return Result<IEnumerable<ObterSolicitacoesTccsDto>>.Success(tccs.MapearParaDto<ObterSolicitacoesTccsDto>());
        }
    }
}
