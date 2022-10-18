using SmartSchool.Comum.Repositorio;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Dominio.Semestres.Especificacao;
using System;
using System.Threading.Tasks;

namespace SmartSchool.Dominio.Semestres.Servicos
{
	public class SemestreServicoDominio : ISemestreServicoDominio
	{
		private readonly IRepositorio<Semestre> _cursoRepositorio;

		public SemestreServicoDominio(IRepositorio<Semestre> semestreRepositorio)
		{
			this._cursoRepositorio = semestreRepositorio;
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
