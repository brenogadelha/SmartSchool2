using MediatR;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Cursos;
using SmartSchool.Dominio.Cursos.Servicos;
using SmartSchool.Dominio.Disciplinas.Servicos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSchool.Aplicacao.Cursos.Adicionar
{
	public class AdicionarCursoHandler : IRequestHandler<AdicionarCursoCommand, IResult>
	{
		private readonly IRepositorio<Curso> _cursoRepositorio;
		private readonly ICursoServicoDominio _cursoServicoDominio;
		private readonly IDisciplinaServicoDominio _disciplinaServicoDominio;

		public AdicionarCursoHandler(IRepositorio<Curso> cursoRepositorio, ICursoServicoDominio cursoServicoDominio, IDisciplinaServicoDominio disciplinaServicoDominio)
		{
			this._cursoRepositorio = cursoRepositorio;
			this._cursoServicoDominio = cursoServicoDominio;
			this._disciplinaServicoDominio = disciplinaServicoDominio;
		}

		public async Task<IResult> Handle(AdicionarCursoCommand request, CancellationToken cancellationToken)
		{
			if (await this._cursoServicoDominio.VerificarExisteCursoComMesmoNome(request.Nome, null))
				throw new ErroNegocioException($"Já existe um Curso com o mesmo nome '{request.Nome}'.");

			foreach (var disciplinaId in request.DisciplinasId)
				await this._disciplinaServicoDominio.ObterAsync(disciplinaId);

			var curso = Curso.Criar(request.Nome, request.DisciplinasId);

			await this._cursoRepositorio.Adicionar(curso);

			return Result<Guid>.Success(curso.Value!.ID);
		}
	}
}
