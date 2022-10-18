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

namespace SmartSchool.Aplicacao.Tccs.Listar
{
    public class ListarTccsQueryHandler : IRequestHandler<ListarTccsQuery, IResult>
    {
        private readonly IRepositorio<Tcc> _tccRepositorio;

        public ListarTccsQueryHandler(IRepositorio<Tcc> tccRepositorio)
        {
            _tccRepositorio = tccRepositorio;
        }

        public async Task<IResult> Handle(ListarTccsQuery request, CancellationToken cancellationToken)
        {
            var tccs = await this._tccRepositorio.Procurar(new BuscaDeTccPorAtivoEspecificacao().IncluiInformacoesDeProfessores());

            return Result<IEnumerable<ObterTccsDto>>.Success(tccs.MapearParaDto<ObterTccsDto>());
        }
    }
}
