using SmartSchool.Comum.Repositorio;
using SmartSchool.Comum.TratamentoErros;
using SmartSchool.Dominio.Tccs.Especificacao;
using System;
using System.Threading.Tasks;

namespace SmartSchool.Dominio.Tccs.Servicos
{
	public class TccServicoDominio : ITccServicoDominio
	{
		private readonly IRepositorio<Tcc> _tccRepositorio;

		public TccServicoDominio(IRepositorio<Tcc> tccRepositorio)
		{
			this._tccRepositorio = tccRepositorio;
		}

		public async Task<Tcc> ObterAsync(Guid tccId)
		{
			if (tccId.Equals(Guid.Empty))
				throw new ArgumentNullException(null, "Id nulo do Tcc (não foi informado).");

			var tcc = await this._tccRepositorio.ObterAsync(new BuscaDeTccPorIdEspecificacao(tccId).IncluiInformacoesDeProfessores());

			if (tcc == null)
				throw new RecursoInexistenteException($"Tcc com ID '{tccId}' não existe.");

			return tcc;
		}

		public async Task<bool> VerificarExisteTccComMesmoTema(string tema, Guid? idAtual)
		{
			var tccComMesmoTema = await this._tccRepositorio.ObterAsync(new BuscaDeTccPorTemaEspecificacao(tema));
			if (tccComMesmoTema != null && (!idAtual.HasValue || idAtual.HasValue && tccComMesmoTema.ID != idAtual))
				return true;

			return false;
		}
	}
}
