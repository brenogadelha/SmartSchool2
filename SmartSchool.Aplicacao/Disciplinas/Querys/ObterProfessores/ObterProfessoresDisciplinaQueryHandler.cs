using MediatR;
using SmartSchool.Comum.Mapeador;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Disciplinas.Servicos;
using SmartSchool.Dto.Dtos.Professores;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSchool.Aplicacao.Disciplinas.ObterProfessores
{
	public class ObterProfessoresDisciplinaQueryHandler : IRequestHandler<ObterProfessoresDisciplinaQuery, IResult>
    {
		private readonly IDisciplinaServicoDominio _disciplinaServicoDominio;

		public ObterProfessoresDisciplinaQueryHandler(IDisciplinaServicoDominio disciplinaServicoDominio)
        {
			this._disciplinaServicoDominio = disciplinaServicoDominio;
        }

        public async Task<IResult> Handle(ObterProfessoresDisciplinaQuery request, CancellationToken cancellationToken)
        {
			var disciplina = await this._disciplinaServicoDominio.ObterAsync(request.Id);

			return Result<IEnumerable<ObterProfessorLightDto>>.Success(disciplina.Professores.MapearParaDto<ObterProfessorLightDto>());
		}
	}
}
