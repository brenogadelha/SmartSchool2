using MediatR;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Disciplinas;
using SmartSchool.Dominio.Disciplinas.Servicos;
using SmartSchool.Dto.Disciplinas;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSchool.Aplicacao.Disciplinas.Adicionar
{
	public class AdicionarDisciplinaHandler : IRequestHandler<AdicionarDisciplinaCommand, IResult>
	{
		private readonly IRepositorio<Disciplina> _disciplinaRepositorio;
		private readonly IDisciplinaServicoDominio _disciplinaServicoDominio;

		public AdicionarDisciplinaHandler(IRepositorio<Disciplina> disciplinaRepositorio, IDisciplinaServicoDominio disciplinaServicoDominio)
		{
			this._disciplinaRepositorio = disciplinaRepositorio;
			this._disciplinaServicoDominio = disciplinaServicoDominio;
		}

		public async Task<IResult> Handle(AdicionarDisciplinaCommand request, CancellationToken cancellationToken)
		{
			if (await this._disciplinaServicoDominio.VerificarExisteDisciplinaComMesmoNome(request.Nome, null))
				throw new ErroNegocioException($"Já existe uma Disciplina com o mesmo nome '{request.Nome}'.");

			var disciplina = Disciplina.Criar(request.Nome, request.Periodo);

			await this._disciplinaRepositorio.Adicionar(disciplina);

			return Result<Guid>.Success(disciplina.Value!.ID);
		}
	}
}
