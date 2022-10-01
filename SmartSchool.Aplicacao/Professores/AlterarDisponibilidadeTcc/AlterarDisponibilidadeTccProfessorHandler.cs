using MediatR;
using SmartSchool.Aplicacao.Professores.Alterar;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Professores;
using SmartSchool.Dominio.Professores.Servicos;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSchool.Aplicacao.Disciplinas.Alterar
{
	public class AlterarDisponibilidadeTccProfessorHandler : IRequestHandler<AlterarDisponibilidadeTccProfessorCommand, IResult>
	{
		private readonly IRepositorio<Professor> _professorRepositorio;
		private readonly IProfessorServicoDominio _professorServicoDominio;

		public AlterarDisponibilidadeTccProfessorHandler(IRepositorio<Professor> professorRepositorio, IProfessorServicoDominio professorServicoDominio)
		{
			this._professorRepositorio = professorRepositorio;
			this._professorServicoDominio = professorServicoDominio;
		}

		public async Task<IResult> Handle(AlterarDisponibilidadeTccProfessorCommand request, CancellationToken cancellationToken)
		{
			var professor = await this._professorServicoDominio.ObterAsync(request.ID);

			professor.AlterarDisponibilidadeTcc(request.DisponibilidadeTcc);

			await this._professorRepositorio.Atualizar(professor, true);

			return Result.Success();
		}
	}
}
