using MediatR;
using SmartSchool.Comum.Repositorio;
using SmartSchool.Comum.Validacao;
using SmartSchool.Dominio.Comum.Results;
using SmartSchool.Dominio.Professores.Servicos;
using SmartSchool.Dominio.Tccs;
using SmartSchool.Dominio.Tccs.Especificacao;
using SmartSchool.Dominio.Tccs.Servicos;
using SmartSchool.Dominio.Tccs.Validacao;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SmartSchool.Aplicacao.Tccs.Alterar
{
	public class AlterarTccHandler : IRequestHandler<AlterarTccCommand, IResult>
	{
		private readonly IRepositorio<Tcc> _tccRepositorio;
		private readonly IRepositorio<TccAlunoProfessor> _tccAlunoProfessorRepositorio;
		private readonly ITccServicoDominio _tccServicoDominio;
		private readonly IProfessorServicoDominio _professorServicoDominio;

		public AlterarTccHandler(IRepositorio<Tcc> tccRepositorio, IRepositorio<TccAlunoProfessor> tccAlunoProfessorRepositorio, ITccServicoDominio tccServicoDominio, IProfessorServicoDominio professorServicoDominio)
		{
			this._tccRepositorio = tccRepositorio;
			this._tccServicoDominio = tccServicoDominio;
			this._professorServicoDominio = professorServicoDominio;
			this._tccAlunoProfessorRepositorio = tccAlunoProfessorRepositorio;
		}

		public async Task<IResult> Handle(AlterarTccCommand request, CancellationToken cancellationToken)
		{
			ValidacaoFabrica.Validar(request, new AlterarTccValidacao());

			if (await this._tccServicoDominio.VerificarExisteTccComMesmoTema(request.Tema, null))
				return Result.UnprocessableEntity($"Já existe um Tcc com o mesmo Tema '{request.Tema}'.");

			// Verifica se professores existem
			foreach (var professorId in request.Professores)
				await this._professorServicoDominio.ObterAsync(professorId);

			// Obtém e verifica se tcc existe
			var tcc = await this._tccServicoDominio.ObterAsync(request.ID);

			var alunosVinculados = await this._tccAlunoProfessorRepositorio.Procurar(new BuscaDeSolicitacaoTccPorIdEspecificacao(tcc.ID));

			if(alunosVinculados.Any())
				return Result.UnprocessableEntity("Não foi possível alterar o tema pois já possui aluno(s) vinculado(s).");

			tcc.AlterarTema(request.Tema);
			tcc.AlterarDescricao(request.Descricao);
			tcc.AtualizarProfessores(request.Professores);

			await this._tccRepositorio.Atualizar(tcc, true);

			return Result.Success();
		}
	}
}
