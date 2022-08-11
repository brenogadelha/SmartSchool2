using MediatR;
using SmartSchool.Comum.Mapeador;
using SmartSchool.Dominio.Alunos.Servicos;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dto.Alunos.Obter;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSchool.Aplicacao.Alunos.ObterAlunoMatricula
{
	public class ObterAlunoMatriculaHandler : IRequestHandler<ObterAlunoMatriculaCommand, IResult>
    {
        private readonly IAlunoServicoDominio _alunoServicoDominio;

        public ObterAlunoMatriculaHandler(IAlunoServicoDominio alunoServicoDominio)
        {
            _alunoServicoDominio = alunoServicoDominio;
        }

        public async Task<IResult> Handle(ObterAlunoMatriculaCommand request, CancellationToken cancellationToken)
        {
            var aluno = await this._alunoServicoDominio.ObterPorMatriculaAsync(request.Matricula);

            return Result<ObterAlunoDto>.Success(aluno.MapearParaDto<ObterAlunoDto>());
        }
    }
}
