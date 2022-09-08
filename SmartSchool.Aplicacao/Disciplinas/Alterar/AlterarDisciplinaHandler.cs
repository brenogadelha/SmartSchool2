using MediatR;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Disciplinas.Servicos;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSchool.Aplicacao.Disciplinas.Alterar
{
	public class AlterarDisciplinaHandler : IRequestHandler<AlterarDisciplinaCommand, IResult>
	{
		private readonly IRepositorio<Disciplina> _disciplinaRepositorio;
		private readonly IDisciplinaServicoDominio _disciplinaServicoDominio;

		public AlterarDisciplinaHandler(IRepositorio<Disciplina> disciplinaRepositorio, IDisciplinaServicoDominio disciplinaServicoDominio)
		{
			this._disciplinaRepositorio = disciplinaRepositorio;
			this._disciplinaServicoDominio = disciplinaServicoDominio;
		}

		public async Task<IResult> Handle(AlterarDisciplinaCommand request, CancellationToken cancellationToken)
		{
			if (await this._disciplinaServicoDominio.VerificarExisteDisciplinaComMesmoNome(request.Nome, request.ID))
				throw new ErroNegocioException($"Já existe uma Disciplina com o mesmo nome '{request.Nome}'.");

			//ValidacaoFabrica.Validar(disciplinaDto, new DisciplinaValidacao());

			var disciplina = await this._disciplinaServicoDominio.ObterAsync(request.ID);

			disciplina.AlterarNome(request.Nome);
			disciplina.AlterarPeriodo(request.Periodo);

			await this._disciplinaRepositorio.Atualizar(disciplina, true);

			return Result.Success();
		}
	}
}
