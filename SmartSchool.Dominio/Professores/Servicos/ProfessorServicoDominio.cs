using SmartSchool.Comum.Repositorio;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Dominio.Professores.Especificacao;
using System;
using System.Threading.Tasks;

namespace SmartSchool.Dominio.Professores.Servicos
{
	public class ProfessorServicoDominio : IProfessorServicoDominio
	{
		private readonly IRepositorio<Professor> _professorRepositorio;

		public ProfessorServicoDominio(IRepositorio<Professor> alunoRepositorio)
		{
			this._professorRepositorio = alunoRepositorio;
		}

		public async Task<Professor> ObterAsync(Guid idProfessor)
		{
			if (idProfessor.Equals(Guid.Empty))
				throw new ArgumentNullException(null, "Id nulo do Professor (não foi informado).");

			var semestre = await this._professorRepositorio.ObterAsync(new BuscaDeProfessorPorIdEspecificacao(idProfessor).IncluiInformacoesDeDisciplina());

			if (semestre == null)
				throw new RecursoInexistenteException($"Professor com ID '{idProfessor}' não existe.");

			return semestre;
		}

		public async Task<bool> VerificarExisteProfessorComMesmaMatricula(int matricula, Guid? idAtual)
		{
			var professorComMesmaMatricula = await this._professorRepositorio.ObterAsync(new BuscaDeProfessorPorMatriculaEspecificacao(matricula));
			if (professorComMesmaMatricula != null && (!idAtual.HasValue || idAtual.HasValue && professorComMesmaMatricula.ID != idAtual))
				return true;

			return false;
		}

		public async Task<bool> VerificarExisteProfessorComMesmoEmail(string email, Guid? idAtual)
		{
			var professorComMesmoEmail = await this._professorRepositorio.ObterAsync(new BuscaDeProfessorPorEmailEspecificacao(email));
			if (professorComMesmoEmail != null && (!idAtual.HasValue || idAtual.HasValue && professorComMesmoEmail.ID != idAtual))
				return true;

			return false;
		}
	}
}
