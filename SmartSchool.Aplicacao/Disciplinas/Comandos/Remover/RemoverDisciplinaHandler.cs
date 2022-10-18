using MediatR;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Disciplinas.Servicos;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSchool.Aplicacao.Disciplinas.Remover
{
	public class RemoverDisciplinaHandler : IRequestHandler<RemoverDisciplinaCommand, IResult>
	{
		private readonly IRepositorio<Disciplina> _disciplinaRepositorio;
		private readonly IDisciplinaServicoDominio _disciplinaServicoDominio;

		public RemoverDisciplinaHandler(IRepositorio<Disciplina> disciplinaRepositorio, IDisciplinaServicoDominio disciplinaServicoDominio)
		{
			this._disciplinaRepositorio = disciplinaRepositorio;
			this._disciplinaServicoDominio = disciplinaServicoDominio;
		}

		public async Task<IResult> Handle(RemoverDisciplinaCommand request, CancellationToken cancellationToken)
		{
			var disciplina = await this._disciplinaServicoDominio.ObterAsync(request.ID);

			disciplina.AlterarAtivo(false);

			await this._disciplinaRepositorio.Atualizar(disciplina, true);

			return Result.Success();
		}
	}
}
