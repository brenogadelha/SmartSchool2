using MediatR;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Semestres;
using SmartSchool.Dominio.Semestres.Servicos;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSchool.Aplicacao.Semestres.Alterar
{
	public class AlterarSemestreHandler : IRequestHandler<AlterarSemestreCommand, IResult>
	{
		private readonly IRepositorio<Semestre> _semestreRepositorio;
		private readonly ISemestreServicoDominio _semestreServicoDominio;

		public AlterarSemestreHandler(IRepositorio<Semestre> semestreRepositorio, ISemestreServicoDominio semestreServicoDominio)
		{
			this._semestreRepositorio = semestreRepositorio;
			this._semestreServicoDominio = semestreServicoDominio;
		}

		public async Task<IResult> Handle(AlterarSemestreCommand request, CancellationToken cancellationToken)
		{
			//ValidacaoFabrica.Validar(semestreDto, new SemestreValidacao());

			var semestre = await this._semestreServicoDominio.ObterAsync(request.ID);

			semestre.AlterarDataInicio(request.DataInicio);
			semestre.AlterarDataFim(request.DataFim);

			await this._semestreRepositorio.Atualizar(semestre, true);

			return Result.Success();
		}
	}
}
