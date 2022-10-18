using MediatR;
using SmartSchool.Comum.Mapeador;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Semestres;
using SmartSchool.Dominio.Semestres.Especificacao;
using SmartSchool.Dto.Semestres;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSchool.Aplicacao.Semestres.Listar
{
    public class ListarSemestresQueryHandler : IRequestHandler<ListarSemestresQuery, IResult>
    {
        private readonly IRepositorio<Semestre> _semestreRepositorio;

        public ListarSemestresQueryHandler(IRepositorio<Semestre> semestreRepositorio)
        {
            _semestreRepositorio = semestreRepositorio;
        }

        public async Task<IResult> Handle(ListarSemestresQuery request, CancellationToken cancellationToken)
        {
            var semestres = await this._semestreRepositorio.Procurar(new BuscaDeSemestrePorAtivoEspecificacao());

            return Result<IEnumerable<ObterSemestreDto>>.Success(semestres.MapearParaDto<ObterSemestreDto>());
        }
    }
}
