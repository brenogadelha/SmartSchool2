using MediatR;
using SmartSchool.Aplicacao.Professores.Alterar;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Disciplinas.Servicos;
using SmartSchool.Dominio.Professores;
using SmartSchool.Dominio.Professores.Servicos;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSchool.Aplicacao.Disciplinas.Alterar
{
	public class AlterarProfessorHandler : IRequestHandler<AlterarProfessorCommand, IResult>
	{
		private readonly IRepositorio<Professor> _professorRepositorio;
		private readonly IProfessorServicoDominio _professorServicoDominio;
		private readonly IDisciplinaServicoDominio _disciplinaServicoDominio;

		public AlterarProfessorHandler(IRepositorio<Professor> professorRepositorio, IProfessorServicoDominio professorServicoDominio, IDisciplinaServicoDominio disciplinaServicoDominio)
		{
			this._professorRepositorio = professorRepositorio;
			this._professorServicoDominio = professorServicoDominio;
			this._disciplinaServicoDominio = disciplinaServicoDominio;
		}

		public async Task<IResult> Handle(AlterarProfessorCommand request, CancellationToken cancellationToken)
		{
			//ValidacaoFabrica.Validar(professorDto, new AlterarProfessorValidacao());

			if (await this._professorServicoDominio.VerificarExisteProfessorComMesmaMatricula(request.Matricula, request.ID))
				throw new ErroNegocioException($"Já existe um Professor com a mesma matricula '{request.Matricula}'.");

			var professor = await this._professorServicoDominio.ObterAsync(request.ID);

			professor.AlterarNome(request.Nome);
			professor.AlterarMatricula(request.Matricula);

			if (request.Disciplinas != null && request.Disciplinas.Any())
			{
				foreach (var disciplina in request.Disciplinas)
					await this._disciplinaServicoDominio.ObterAsync(disciplina);
				professor.AtualizarDisciplinas(request.Disciplinas);
			}

			await this._professorRepositorio.Atualizar(professor, true);

			return Result.Success();
		}
	}
}
