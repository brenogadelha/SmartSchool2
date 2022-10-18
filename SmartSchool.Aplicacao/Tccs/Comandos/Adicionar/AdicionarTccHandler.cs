using MediatR;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Professores.Servicos;
using SmartSchool.Dominio.Tccs;
using SmartSchool.Dominio.Tccs.Servicos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSchool.Aplicacao.Tccs.Adicionar
{
	public class AdicionarTccHandler : IRequestHandler<AdicionarTccCommand, IResult>
	{
		private readonly IRepositorio<Tcc> _tccRepositorio;
		private readonly ITccServicoDominio _tccServicoDominio;
		private readonly IProfessorServicoDominio _professorServicoDominio;

		public AdicionarTccHandler(IRepositorio<Tcc> tccRepositorio, ITccServicoDominio tccServicoDominio, IProfessorServicoDominio professorServicoDominio)
		{
			this._tccRepositorio = tccRepositorio;
			this._tccServicoDominio = tccServicoDominio;
			this._professorServicoDominio = professorServicoDominio;
		}

		public async Task<IResult> Handle(AdicionarTccCommand request, CancellationToken cancellationToken)
		{
			if (await this._tccServicoDominio.VerificarExisteTccComMesmoTema(request.Tema, null))
				return Result.UnprocessableEntity($"Já existe um Tcc com o mesmo Tema '{request.Tema}'.");

			// Verifica se professores existem
			foreach (var professorId in request.Professores)
				await this._professorServicoDominio.ObterAsync(professorId);

			var tcc = Tcc.Criar(request.Tema, request.Descricao, request.Professores);

			await this._tccRepositorio.Adicionar(tcc);

			return Result<Guid>.Success(tcc.Value!.ID);			
		}
	}
}
