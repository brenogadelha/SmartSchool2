using MediatR;
using SmartSchool.Comum.Mapeador;
using SmartSchool.Dominio.Alunos.Servicos;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dto.Alunos.Obter;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSchool.Aplicacao.Alunos.ObterPorMatricula
{
	public class ObterAlunoMatriculaQueryHandler : IRequestHandler<ObterAlunoMatriculaQuery, IResult>
    {
        private readonly IAlunoServicoDominio _alunoServicoDominio;

        public ObterAlunoMatriculaQueryHandler(IAlunoServicoDominio alunoServicoDominio)
        {
            _alunoServicoDominio = alunoServicoDominio;
        }

        public async Task<IResult> Handle(ObterAlunoMatriculaQuery request, CancellationToken cancellationToken)
        {
            var aluno = await this._alunoServicoDominio.ObterPorMatriculaAsync(request.Matricula);

            return Result<ObterAlunoDto>.Success(aluno.MapearParaDto<ObterAlunoDto>());
        }
    }
}
