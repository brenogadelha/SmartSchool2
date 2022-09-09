using MediatR;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Dominio.Alunos;
using SmartSchool.Dominio.Alunos.Servicos;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Professores.Servicos;
using SmartSchool.Dominio.Tccs;
using SmartSchool.Dominio.Tccs.Servicos;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSchool.Aplicacao.Tccs.Solicitar
{
	public class SolicitarTccHandler : IRequestHandler<SolicitarTccCommand, IResult>
	{
		private readonly IRepositorio<Aluno> _alunoRepositorio;
		private readonly ITccServicoDominio _tccServicoDominio;
		private readonly IProfessorServicoDominio _professorServicoDominio;
		private readonly IAlunoServicoDominio _AlunoServicoDominio;

		public SolicitarTccHandler(IRepositorio<Aluno> alunoRepositorio, ITccServicoDominio tccServicoDominio,
			IProfessorServicoDominio professorServicoDominio, IAlunoServicoDominio AlunoServicoDominio)
		{
			this._alunoRepositorio = alunoRepositorio;
			this._tccServicoDominio = tccServicoDominio;
			this._professorServicoDominio = professorServicoDominio;
			this._AlunoServicoDominio = AlunoServicoDominio;
		}

		public async Task<IResult> Handle(SolicitarTccCommand request, CancellationToken cancellationToken)
		{
			// Verifica se tcc existe
			await this._tccServicoDominio.ObterAsync(request.TccId);

			// Verifica se professor existe
			await this._professorServicoDominio.ObterAsync(request.ProfessorId);

			if (request.AlunosIds.Count > 3)
				throw new ErroNegocioException("O grupo para o TCC deve ser formado por no máximo 3 alunos.");

			foreach (var alunoId in request.AlunosIds.ToList())
			{
				var aluno = await this._AlunoServicoDominio.ObterPorIdAsync(alunoId);

				// Caso haja alguma solicitação em andamento, a mesma é desconsiderada após nova solicitação
				if (aluno.TccsProfessores.Any())
					aluno.TccsProfessores.Clear();

				aluno.TccsProfessores.Add(TccAlunoProfessor.Criar(request.TccId, request.ProfessorId, alunoId, request.Solicitacao));

				await this._alunoRepositorio.Atualizar(aluno, true);
			}

			return Result.Success();
		}
	}
}
