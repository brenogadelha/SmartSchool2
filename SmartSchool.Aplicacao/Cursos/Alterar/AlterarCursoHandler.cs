using MediatR;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Cursos;
using SmartSchool.Dominio.Cursos.Servicos;
using SmartSchool.Dominio.Disciplinas.Servicos;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSchool.Aplicacao.Cursos.Alterar
{
	public class AlterarCursoHandler : IRequestHandler<AlterarCursoCommand, IResult>
	{
		private readonly IRepositorio<Curso> _cursoRepositorio;
		private readonly ICursoServicoDominio _cursoServicoDominio;
		private readonly IDisciplinaServicoDominio _disciplinaServicoDominio;

		public AlterarCursoHandler(IRepositorio<Curso> alunoRepositorio, ICursoServicoDominio cursoServicoDominio, IDisciplinaServicoDominio disciplinaServicoDominio)
		{
			this._cursoRepositorio = alunoRepositorio;
			this._cursoServicoDominio = cursoServicoDominio;
			this._disciplinaServicoDominio = disciplinaServicoDominio;
		}

		public async Task<IResult> Handle(AlterarCursoCommand request, CancellationToken cancellationToken)
		{
			if (await this._cursoServicoDominio.VerificarExisteCursoComMesmoNome(request.Nome, request.ID))
				throw new ErroNegocioException($"Já existe um Curso com o mesmo nome '{request.Nome}'.");

			var curso = await this._cursoServicoDominio.ObterAsync(request.ID);

			//ValidacaoFabrica.Validar(cursoDto, new CursoValidacao());

			foreach (var disciplinaId in request.DisciplinasId)
				await this._disciplinaServicoDominio.ObterAsync(disciplinaId);

			curso.AlterarNome(request.Nome);
			curso.AtualizarDisciplinas(request.DisciplinasId);

			await this._cursoRepositorio.Atualizar(curso, true);

			return Result.Success();
		}
	}
}
