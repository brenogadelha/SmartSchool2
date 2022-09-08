using MediatR;
using SmartSchool.Comum.Mapeador;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Professores;
using SmartSchool.Dominio.Professores.Especificacao;
using SmartSchool.Dto.Dtos.Professores;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSchool.Aplicacao.Professores.Listar
{
    public class ListarProfessoresHandler : IRequestHandler<ListarProfessoresCommand, IResult>
    {
        private readonly IRepositorio<Professor> _professorRepositorio;

        public ListarProfessoresHandler(IRepositorio<Professor> professorRepositorio)
        {
            _professorRepositorio = professorRepositorio;
        }

        public async Task<IResult> Handle(ListarProfessoresCommand request, CancellationToken cancellationToken)
        {
            var professores = await this._professorRepositorio.Procurar(new BuscaDeProfessorPorAtivoEspecificacao().IncluiInformacoesDeDisciplina());

            return Result<IEnumerable<ObterProfessorDto>>.Success(professores.MapearParaDto<ObterProfessorDto>());
        }
    }
}
