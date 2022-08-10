using System;
using System.Threading.Tasks;

namespace SmartSchool.Dominio.Semestres.Servicos
{
	public interface ISemestreServicoDominio
	{
		Task<Semestre> ObterAsync(Guid idAtividade);
	}
}
