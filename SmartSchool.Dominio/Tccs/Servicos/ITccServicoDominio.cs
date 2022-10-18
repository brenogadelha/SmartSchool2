using System;
using System.Threading.Tasks;

namespace SmartSchool.Dominio.Tccs.Servicos
{
	public interface ITccServicoDominio
	{
		Task<Tcc> ObterAsync(Guid tccId);
		Task<bool> VerificarExisteTccComMesmoTema(string tema, Guid? idAtual);
	}
}
