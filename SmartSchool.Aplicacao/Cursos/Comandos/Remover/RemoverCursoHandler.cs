using MediatR;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Cursos;
using SmartSchool.Dominio.Cursos.Servicos;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSchool.Aplicacao.Cursos.Remover
{
	public class RemoverCursoHandler : IRequestHandler<RemoverCursoCommand, IResult>
	{
		private readonly IRepositorio<Curso> _cursoRepositorio;
		private readonly ICursoServicoDominio _cursoServicoDominio;

		public RemoverCursoHandler(IRepositorio<Curso> alunoRepositorio, ICursoServicoDominio cursoServicoDominio)
		{
			this._cursoRepositorio = alunoRepositorio;
			this._cursoServicoDominio = cursoServicoDominio;
		}

		public async Task<IResult> Handle(RemoverCursoCommand request, CancellationToken cancellationToken)
		{
			var curso = await this._cursoServicoDominio.ObterAsync(request.ID);

			curso.AlterarAtivo(false);

			await this._cursoRepositorio.Atualizar(curso, true);

			return Result.Success();
		}
	}
}
