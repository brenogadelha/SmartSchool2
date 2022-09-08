using MediatR;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Semestres;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSchool.Aplicacao.Semestres.Adicionar
{
	public class AdicionarSemestreHandler : IRequestHandler<AdicionarSemestreCommand, IResult>
	{
		private readonly IRepositorio<Semestre> _semestreRepositorio;

		public AdicionarSemestreHandler(IRepositorio<Semestre> semestreRepositorio)
		{
			this._semestreRepositorio = semestreRepositorio;
		}

		public async Task<IResult> Handle(AdicionarSemestreCommand request, CancellationToken cancellationToken)
		{
			var semestre = Semestre.Criar(request.DataInicio, request.DataFim);

			await this._semestreRepositorio.Adicionar(semestre);

			return Result<Guid>.Success(semestre.Value!.ID);			
		}
	}
}
