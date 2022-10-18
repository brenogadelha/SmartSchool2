using MediatR;
using SmartSchool.Comum.Mapeador;
using SmartSchool.Dominio.Alunos.Servicos;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dto.Alunos.Obter;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSchool.Aplicacao.Alunos.ObterHistorico
{
	public class ObterHistoricoAlunoQueryHandler : IRequestHandler<ObterHistoricoAlunoQuery, IResult>
	{
		private readonly IAlunoServicoDominio _alunoServicoDominio;

		public ObterHistoricoAlunoQueryHandler(IAlunoServicoDominio alunoServicoDominio)
		{
			_alunoServicoDominio = alunoServicoDominio;
		}

		public async Task<IResult> Handle(ObterHistoricoAlunoQuery request, CancellationToken cancellationToken)
		{
			var aluno = await this._alunoServicoDominio.ObterPorIdAsync(request.Id);

			var alunoHistoricoDto = aluno.SemestresDisciplinas.MapearParaDto<ObterHistoricoAlunoDto>();

			if (request.Periodo.HasValue)
				return Result<IEnumerable<ObterHistoricoAlunoDto>>.Success(alunoHistoricoDto.OrderByDescending(historico => historico.Periodo).Where(s => s.Periodo == request.Periodo));

			else return Result<IEnumerable<ObterHistoricoAlunoDto>>.Success(alunoHistoricoDto.OrderByDescending(historico => historico.Periodo));
		}
	}
}
