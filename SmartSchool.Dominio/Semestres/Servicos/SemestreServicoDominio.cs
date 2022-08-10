using SmartSchool.Comum.Repositorio;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Dominio.Semestres.Especificacao;
using System;
using System.Threading.Tasks;

namespace SmartSchool.Dominio.Semestres.Servicos
{
	public class SemestreServicoDominio : ISemestreServicoDominio
	{
		private readonly IRepositorioTask<Semestre> _cursoRepositorio;

		public SemestreServicoDominio(IRepositorioTask<Semestre> alunoRepositorio)
		{
			this._cursoRepositorio = alunoRepositorio;
		}

		public async Task<Semestre> ObterAsync(Guid idSemestre)
		{
			if (idSemestre.Equals(Guid.Empty))
				throw new ArgumentNullException(null, "Id nulo do Semestre (não foi informado).");

			var semestre = await this._cursoRepositorio.ObterAsync(new BuscaDeSemestrePorIdEspecificacao(idSemestre));

			if (semestre == null)
				throw new RecursoInexistenteException($"Semestre com ID '{idSemestre}' não existe.");

			return semestre;
		}
	}
}
