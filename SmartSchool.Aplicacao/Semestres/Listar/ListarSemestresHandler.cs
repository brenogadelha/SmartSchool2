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
    public class ListarSemestresHandler : IRequestHandler<ListarSemestresCommand, IResult>
    {
        private readonly IRepositorio<Semestre> _semestreRepositorio;

        public ListarSemestresHandler(IRepositorio<Semestre> semestreRepositorio)
        {
            _semestreRepositorio = semestreRepositorio;
        }

        public async Task<IResult> Handle(ListarSemestresCommand request, CancellationToken cancellationToken)
        {
            var semestres = await this._semestreRepositorio.Procurar(new BuscaDeSemestrePorAtivoEspecificacao());

            return Result<IEnumerable<ObterSemestreDto>>.Success(semestres.MapearParaDto<ObterSemestreDto>());
        }
    }
}
