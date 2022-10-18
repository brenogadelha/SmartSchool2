using MediatR;
using SmartSchool.Comum.Mapeador;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dominio.Alunos;
using SmartSchool.Dominio.Alunos.Especificacao;
using SmartSchool.Dominio.Alunos.Servicos;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dto.Alunos.Obter;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSchool.Aplicacao.Alunos.ObterPorId
{
    public class ObterAlunoQueryHandler : IRequestHandler<ObterAlunoQuery, IResult>
    {
		private readonly IAlunoServicoDominio _alunoServicoDominio;

		public ObterAlunoQueryHandler(IAlunoServicoDominio alunoServicoDominio)
        {
			this._alunoServicoDominio = alunoServicoDominio;
        }

        public async Task<IResult> Handle(ObterAlunoQuery request, CancellationToken cancellationToken)
        {
			var aluno = await this._alunoServicoDominio.ObterPorIdAsync(request.Id);

			return Result<ObterAlunoDto>.Success(aluno.MapearParaDto<ObterAlunoDto>());
        }
    }
}
