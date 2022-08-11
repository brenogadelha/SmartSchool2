using MediatR;
using SmartSchool.Comum.Mapeador;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dominio.Alunos;
using SmartSchool.Dominio.Alunos.Especificacao;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dto.Alunos.Obter;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSchool.Aplicacao.Alunos.ObterAluno
{
    public class ObterAlunoHandler : IRequestHandler<ObterAlunoCommand, IResult>
    {
        private readonly IRepositorioTask<Aluno> _alunoRepositorio;

        public ObterAlunoHandler(IRepositorioTask<Aluno> alunoRepositorio)
        {
            _alunoRepositorio = alunoRepositorio;
        }

        public async Task<IResult> Handle(ObterAlunoCommand request, CancellationToken cancellationToken)
        {
            var aluno = await _alunoRepositorio.ObterAsync(new BuscaDeAlunoPorIdEspecificacao(request.Id).IncluiInformacoesDeHistorico().IncluiInformacoesDeDisciplina().IncluiInformacoesDeCurso());

            if (aluno == null)
                return Result.NotFound();

            return Result<ObterAlunoDto>.Success(aluno.MapearParaDto<ObterAlunoDto>());
        }
    }
}
