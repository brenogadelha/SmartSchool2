using MediatR;
using SmartSchool.Aplicacao.Alunos.ListarAlunos;
using SmartSchool.Comum.Mapeador;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dominio.Alunos;
using SmartSchool.Dominio.Alunos.Especificacao;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dto.Alunos.Obter;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Parceiros.Template.Aplicacao.Pessoas.ListarPessoas
{
    public class ListarAlunosHandler : IRequestHandler<ListarAlunosCommand, IResult>
    {
        private readonly IRepositorioTask<Aluno> _alunoRepositorio;

        public ListarAlunosHandler(IRepositorioTask<Aluno> alunoRepositorio)
        {
            _alunoRepositorio = alunoRepositorio;
        }

        public async Task<IResult> Handle(ListarAlunosCommand request, CancellationToken cancellationToken)
        {
            var alunos = await this._alunoRepositorio.Procurar(new BuscaDeAlunoPorAtivoEspecificacao(true).IncluiInformacoesDeCurso());

            var alunosDto = alunos.MapearParaDto<ObterAlunoDto>();

            return Result<IEnumerable<ObterAlunoDto>>.Success(alunosDto);
        }
    }
}
