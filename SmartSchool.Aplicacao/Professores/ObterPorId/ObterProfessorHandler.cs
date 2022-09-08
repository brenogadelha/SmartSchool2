using MediatR;
using SmartSchool.Comum.Mapeador;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Professores.Servicos;
using SmartSchool.Dto.Dtos.Professores;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSchool.Aplicacao.Professores.ObterPorId
{
    public class ObterProfessorHandler : IRequestHandler<ObterProfessorCommand, IResult>
    {
		private readonly IProfessorServicoDominio _professorServicoDominio;

		public ObterProfessorHandler(IProfessorServicoDominio professorServicoDominio)
        {
			this._professorServicoDominio = professorServicoDominio;
        }

        public async Task<IResult> Handle(ObterProfessorCommand request, CancellationToken cancellationToken)
        {
			var professor = await this._professorServicoDominio.ObterAsync(request.Id);

			return Result<ObterProfessorDto>.Success(professor.MapearParaDto<ObterProfessorDto>());
        }
    }
}
