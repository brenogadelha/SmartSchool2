using MediatR;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dominio.Alunos;
using SmartSchool.Dominio.Alunos.Servicos;
using SmartSchool.Dominio.Comum.Results;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSchool.Aplicacao.Alunos.RemoverAluno
{
	public class RemoverAlunoHandler : IRequestHandler<RemoverAlunoCommand, IResult>
	{
		private readonly IRepositorioTask<Aluno> _alunoRepositorio;
		private readonly IAlunoServicoDominio _alunoServicoDominio;

		public RemoverAlunoHandler(IRepositorioTask<Aluno> alunoRepositorio, IAlunoServicoDominio alunoServicoDominio)
		{
			this._alunoRepositorio = alunoRepositorio;
			this._alunoServicoDominio = alunoServicoDominio;
		}

		public async Task<IResult> Handle(RemoverAlunoCommand request, CancellationToken cancellationToken)
		{
			var aluno = await this._alunoServicoDominio.ObterPorIdAsync(request.ID);

			aluno.AlterarAtivo(false);

			await this._alunoRepositorio.Atualizar(aluno);

			return Result.Success();
		}
	}
}
