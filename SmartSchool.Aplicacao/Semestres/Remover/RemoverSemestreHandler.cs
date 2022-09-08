using MediatR;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Semestres;
using SmartSchool.Dominio.Semestres.Servicos;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSchool.Aplicacao.Semestres.Remover
{
	public class RemoverSemestreHandler : IRequestHandler<RemoverSemestreCommand, IResult>
	{
		private readonly IRepositorio<Semestre> _semestreRepositorio;
		private readonly ISemestreServicoDominio _semestreServicoDominio;

		public RemoverSemestreHandler(IRepositorio<Semestre> professorRepositorio, ISemestreServicoDominio semestreServicoDominio)
		{
			this._semestreRepositorio = professorRepositorio;
			this._semestreServicoDominio = semestreServicoDominio;
		}

		public async Task<IResult> Handle(RemoverSemestreCommand request, CancellationToken cancellationToken)
		{
			var semestre = await this._semestreServicoDominio.ObterAsync(request.ID);

			semestre.AlterarAtivo(false);

			await this._semestreRepositorio.Atualizar(semestre, true);

			return Result.Success();
		}
	}
}
