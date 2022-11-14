using MediatR;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Disciplinas.Servicos;
using SmartSchool.Dominio.Professores;
using SmartSchool.Dominio.Professores.Servicos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSchool.Aplicacao.Professores.Adicionar
{
	public class AdicionarProfessorHandler : IRequestHandler<AdicionarProfessorCommand, IResult>
	{
		private readonly IRepositorio<Professor> _professorRepositorio;
		private readonly IProfessorServicoDominio _professorServicoDominio;
		private readonly IDisciplinaServicoDominio _disciplinaServicoDominio;

		public AdicionarProfessorHandler(IRepositorio<Professor> professorRepositorio, IProfessorServicoDominio professorServicoDominio, IDisciplinaServicoDominio disciplinaServicoDominio)
		{
			this._professorRepositorio = professorRepositorio;
			this._professorServicoDominio = professorServicoDominio;
			this._disciplinaServicoDominio = disciplinaServicoDominio;
		}

		public async Task<IResult> Handle(AdicionarProfessorCommand request, CancellationToken cancellationToken)
		{
			if (await this._professorServicoDominio.VerificarExisteProfessorComMesmaMatricula(request.Matricula, null))
				return Result.UnprocessableEntity($"Já existe um Professor com a mesma matricula '{request.Matricula}'.");

			if (await this._professorServicoDominio.VerificarExisteProfessorComMesmoEmail(request.Email, null))
				return Result.UnprocessableEntity($"Já existe um Professor com o mesmo email '{request.Email}'.");

			if (request.Disciplinas != null)
			{
				foreach (var disciplina in request.Disciplinas)
					await this._disciplinaServicoDominio.ObterAsync(disciplina);
			}

			var professor = Professor.Criar(request.Nome, request.Matricula, request.Email, request.Disciplinas);

			await this._professorRepositorio.Adicionar(professor);

			return Result<Guid>.Success(professor.Value!.ID);
		}
	}
}
