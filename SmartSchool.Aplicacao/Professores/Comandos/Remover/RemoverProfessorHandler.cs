using MediatR;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Professores;
using SmartSchool.Dominio.Professores.Servicos;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSchool.Aplicacao.Professores.Remover
{
	public class RemoverProfessorHandler : IRequestHandler<RemoverProfessorCommand, IResult>
	{
		private readonly IRepositorio<Professor> _professorRepositorio;
		private readonly IProfessorServicoDominio _professorServicoDominio;

		public RemoverProfessorHandler(IRepositorio<Professor> professorRepositorio, IProfessorServicoDominio professorServicoDominio)
		{
			this._professorRepositorio = professorRepositorio;
			this._professorServicoDominio = professorServicoDominio;
		}

		public async Task<IResult> Handle(RemoverProfessorCommand request, CancellationToken cancellationToken)
		{
			var professor = await this._professorServicoDominio.ObterAsync(request.ID);

			professor.AlterarAtivo(false);

			await this._professorRepositorio.Atualizar(professor, true);

			return Result.Success();
		}
	}
}
