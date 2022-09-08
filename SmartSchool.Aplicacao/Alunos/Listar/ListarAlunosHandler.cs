using MediatR;
using SmartSchool.Comum.Mapeador;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dominio.Alunos;
using SmartSchool.Dominio.Alunos.Especificacao;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dto.Alunos.Obter;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSchool.Aplicacao.Alunos.Listar
{
    public class ListarAlunosHandler : IRequestHandler<ListarAlunosCommand, IResult>
    {
        private readonly IRepositorio<Aluno> _alunoRepositorio;

        public ListarAlunosHandler(IRepositorio<Aluno> alunoRepositorio)
        {
            _alunoRepositorio = alunoRepositorio;
        }

        public async Task<IResult> Handle(ListarAlunosCommand request, CancellationToken cancellationToken)
        {
            var alunos = await this._alunoRepositorio.Procurar(new BuscaDeAlunoPorAtivoEspecificacao(true).IncluiInformacoesDeCurso());

            return Result<IEnumerable<ObterAlunoDto>>.Success(alunos.MapearParaDto<ObterAlunoDto>());
        }
    }
}
